using UnityEngine;
using UnityEngine.InputSystem;

namespace Movables
{
    public class ButtonInteraction : MonoBehaviour
    {
        [SerializeField] private ButtonMovable[] _currentDoors; 
        
        private bool _isPlayerInRange;  
        
        void Update()
        {
            if (!_isPlayerInRange) return;
            if (!Keyboard.current.eKey.wasReleasedThisFrame) return;
            foreach (var buttonMovable in _currentDoors)
            {
                if (buttonMovable)
                {
                    buttonMovable.ToggleDoor(); 
                }
                else
                {
                            
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Player entered button's trigger zone BUTTON INTERACTION!");

            var buttonMovable = gameObject.GetComponents<ButtonMovable>();

            if (buttonMovable == null) return;
            _currentDoors = buttonMovable;
                       
            _isPlayerInRange = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Player exited button's trigger zone! BUTTON INTERACTION");
            _isPlayerInRange = false;

            _currentDoors = null;
        }
    }
}