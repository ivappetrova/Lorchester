using UnityEngine;
using System.Collections;

namespace Movables
{
    public class ButtonMovable : MonoBehaviour
    {
        [SerializeField] private GameObject _door; 

        private float _elapsedTime;
        private bool _isMoving;
        private bool _isDoorOpen;  
        public float DoorOpenHeight = 3f; 
        public float DoorClosedHeight; 

        public void ToggleDoor()
        {
            Debug.Log("Toggling door state.");

            if (_isDoorOpen&& !_isMoving)
            {
                // Close the door
                _elapsedTime = 0f;
                StartCoroutine(MoveDoor(DoorClosedHeight));
                
                _isDoorOpen = !_isDoorOpen;
            }
            else if (!_isMoving)
            {
                // Open the door
                _elapsedTime = 0f;
                StartCoroutine(MoveDoor(DoorOpenHeight));
               
                _isDoorOpen = !_isDoorOpen;
            }
        }

        private IEnumerator MoveDoor(float targetHeight)
        {
            _isMoving = true;
            float duration = 2f; 

            Vector3 startingPosition = _door.transform.position;
            Vector3 targetPosition = new Vector3(_door.transform.position.x, targetHeight, _door.transform.position.z);

            while (_elapsedTime < duration)
            {
                _door.transform.position = Vector3.Lerp(startingPosition, targetPosition, (_elapsedTime / duration));
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            _door.transform.position = targetPosition; 
            _isMoving = false;
        }
    }
}
