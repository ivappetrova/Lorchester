using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    [RequireComponent(typeof(SoundEmitter))]
    public class Health : MonoBehaviour
    {
        [Header("Lives")]
        [SerializeField] private int startLives = 3;
        [Tooltip("Minimum seconds between hits. Stops one overlap from counting as multiple hits.")]
        [SerializeField] private float damageCooldown = 1f;

        [Header("Damage Flash")]
        [SerializeField] private Renderer bodyRenderer;
        [SerializeField] private Renderer shoulderRenderer;
        [SerializeField] private Renderer companionRenderer; // optional
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private float colorChangeDuration = 0.2f;

        [Header("Sounds")]
        [Tooltip("One is picked at random on every non-lethal hit.")]
        [SerializeField] private AudioClip[] damageSounds;
        [SerializeField] private AudioClip deathSound;

        private const float ReloadDelay = 2f;

        private int _currentLives;
        private float _lastDamageTime = -Mathf.Infinity;
        private bool _isGameOver;

        private PlayerCharacter _player;
        private SoundEmitter _sound;
        private Coroutine _flashRoutine;

        private Renderer[] _renderers;
        private Color[] _originalColors;

        public int StartHealth => startLives;
        public int CurrentHealth => _currentLives;
        public int CurrentLives => _currentLives;
        public bool IsGameOver => _isGameOver;

        public delegate void HealthChange(float startHealth, float currentHealth);
        public event HealthChange OnHealthChanged;

        private void Awake()
        {
            _currentLives = startLives;
            _player = GetComponent<PlayerCharacter>();
            _sound = GetComponent<SoundEmitter>();

            CacheRenderers();
        }

        public void TakeDamage(int damageAmount)
        {
            if (_isGameOver) return;
            if (Time.time - _lastDamageTime < damageCooldown) return;
            _lastDamageTime = Time.time;

            _currentLives -= damageAmount;
            OnHealthChanged?.Invoke(startLives, _currentLives);

            if (_currentLives > 0)
                OnDamaged();
            else
                Die();
        }

        private void OnDamaged()
        {
            PlayRandomDamageSound();

            if (_flashRoutine != null) StopCoroutine(_flashRoutine);
            _flashRoutine = StartCoroutine(FlashDamageColor());
        }

        private void Die()
        {
            _isGameOver = true;

            if (_player != null) _player.enabled = false;

            if (_flashRoutine != null) StopCoroutine(_flashRoutine);
            SetColors(damageColor);

            _sound.Play(deathSound);

            StartCoroutine(ReloadScene());
        }

        private void PlayRandomDamageSound()
        {
            if (damageSounds == null || damageSounds.Length == 0) return;

            var clip = damageSounds[Random.Range(0, damageSounds.Length)];
            _sound.Play(clip);
        }

        private IEnumerator FlashDamageColor()
        {
            SetColors(damageColor);
            yield return new WaitForSeconds(colorChangeDuration);
            RestoreColors();
        }

        private IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(ReloadDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // ---- Color helpers ----

        private void CacheRenderers()
        {
            // Companion is optional, so null entries are simply skipped.
            _renderers = new[] { bodyRenderer, shoulderRenderer, companionRenderer };
            _originalColors = new Color[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i] != null)
                    _originalColors[i] = _renderers[i].material.color;
            }
        }

        private void SetColors(Color color)
        {
            foreach (var r in _renderers)
            {
                if (r != null) r.material.color = color;
            }
        }

        private void RestoreColors()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i] != null)
                    _renderers[i].material.color = _originalColors[i];
            }
        }
    }
}