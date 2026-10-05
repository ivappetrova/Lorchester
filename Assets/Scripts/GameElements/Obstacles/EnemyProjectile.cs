using Player;
using UnityEngine;
using Utils;

namespace GameElements.Obstacles
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 20.0f;
        [SerializeField] private float _lifeTime = 5.0f;

        private Vector3 _moveDirection;
        private Rigidbody _rb;
        private CharacterControlSwitcher _controlSwitcher;
        private Health _sharedHealth;

        void Awake()
        {
            _sharedHealth = FindAnyObjectByType<Health>();
            _controlSwitcher = FindAnyObjectByType<CharacterControlSwitcher>();
            _rb = GetComponent<Rigidbody>();
            gameObject.tag = "EnemyBullet";
            Invoke(nameof(Kill), _lifeTime);
            _moveDirection = transform.up;
        }

        void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _moveDirection * (Time.fixedDeltaTime * _speed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                if (_sharedHealth)
                {
                    Debug.Log("Player took dmg");
                    _sharedHealth.TakeDamage(1);
                }
                Kill();
            }
            else if (other.name == "Companion")
            {
                if (_sharedHealth)
                {
                    _sharedHealth.TakeDamage(1);
                    Debug.Log("Companion took dmg");
                    if (_controlSwitcher)
                    {
                        _controlSwitcher.AutoSwitchToPlayer();
                    }
                }
                Kill();
            }
            else if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Door") || other.CompareTag("Destructible") )
            {
                Kill();
            }
        }

        void Kill()
        {
            Destroy(gameObject);
        }
    }
}