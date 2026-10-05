using UnityEngine;
using Weapon;

namespace Player
{
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _gunTemplate;  
        [SerializeField] private GameObject _socket; 

        private BasicWeapon _weapon;
        private Transform _weaponTransform;  
        public Transform WeaponTransform => _weaponTransform;

        void Awake()
        {
            // Spawn the weapon
            if (!_gunTemplate || !_socket) return;
            var gunObject = Instantiate(_gunTemplate, _socket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;

            _weapon = gunObject.GetComponent<BasicWeapon>();
            _weaponTransform = gunObject.transform;
        }

        public void Attack()
        {
            if (_weapon)
            {
                _weapon.Fire();
            }
        }
    }
}