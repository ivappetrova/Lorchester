using UnityEngine;

namespace Movables
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private Door _targetDoor; 
        [SerializeField] private bool _isCollected;
        public Door TargetDoor => _targetDoor;
        public bool IsCollected => _isCollected;
        
        public void SetTargetDoor(Door door)
        {
            _targetDoor = door;
        }

        public void CollectKey()
        {
            if (!_isCollected)
            {
                _isCollected = true;
                Debug.Log("Key has been collected!");
                gameObject.SetActive(false); // Hide the key after collection

                if (_targetDoor)
                {
                    _targetDoor.UnlockDoor();  // Unlock only the assigned door
                }
                else
                {
                    Debug.LogWarning("No door assigned to this key!");
                }
            }
            else
            {
                Debug.Log("Key was already collected.");
            }
        }
    }
}
