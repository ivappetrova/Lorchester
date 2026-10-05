using UnityEngine;
using Utils;

namespace GameElements.Obstacles
{
    public class ShootingObstacle : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 2.0f;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _bulletSocket;
        
        private SoundEmitter _soundEmitter;
        private float _shootTimer;

        private void Awake()
        { 
            _soundEmitter = GetComponent<SoundEmitter>();

            // It's off until a ZoneController explicitly activates this shooter
            enabled = false;
        }

        private void Update()
        {
            _shootTimer -= Time.deltaTime;

            if (!(_shootTimer <= 0.0f)) return;
            ShootProjectile();
            _shootTimer = _shootInterval;
        }

        // Called by ZoneController when the player crosses this zone's entrance/exit.
        public void SetActive(bool isActive)
        {
            Debug.Log($"[{name}] SetActive({isActive}) called - enabled was {enabled}");
            enabled = isActive;

            if (isActive)
            {
                _shootTimer = 0f;
            }
        }

        private void ShootProjectile()
        {
            if (!_projectilePrefab || !_bulletSocket) return;
            Instantiate(_projectilePrefab, _bulletSocket.position, _bulletSocket.rotation);
            _soundEmitter?.PlaySound();
        }
    }
}