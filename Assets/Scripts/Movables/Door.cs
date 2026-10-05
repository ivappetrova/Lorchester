using UnityEngine;

namespace Movables
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float _moveHeight = 10f;
        [SerializeField] private float _moveSpeed = 2f;
        
        private bool _isUnlocked;
        private bool _isOpen;
        private Vector3 _closedPosition;
        private Vector3 _openPosition;
        
        void Start()
        {
            _closedPosition = transform.position;
            _openPosition = _closedPosition + Vector3.up * _moveHeight;
        }

        void Update()
        {
            // If unlocked and open, move the door upwards
            if (_isUnlocked && !_isOpen && transform.position.y < _openPosition.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, _openPosition, _moveSpeed * Time.deltaTime);
            }
           
        }

        public virtual void UnlockDoor()
        {
            if (_isUnlocked) return;
            _isUnlocked = true;
            Debug.Log("Unlocking the door!");
        }
    }
}