using UnityEngine;

namespace Player
{
    public class CharacterControlSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _player;     
        [SerializeField] private CompanionFollow _companion;  

        private void Start()
        {
            // Ensure the game begins with the player in control and the companion snapped
            // into its proper follow position, instead of wherever it was left in the editor.
            SwitchToPlayer();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchToPlayer();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToCompanion();
            }
        }

        private void SwitchToPlayer()
        {
            if (!_player || !_companion) return;
            _player.EnableControl();
            _companion.SetPlayer(_player.transform);  
            _companion.SetFollowMode();              
            Debug.Log("Switched control to the player.");
        }

        private void SwitchToCompanion()
        {
            if (!_player || !_companion) return;
            _companion.SetFlyingMode();
            _player.DisableControl();
            Debug.Log("Switched control to the companion.");
        }

        // Method to auto-switch control to the player
        public void AutoSwitchToPlayer()
        {
            Debug.Log("Automatically switching control back to the player.");
            SwitchToPlayer();
        }
    }
}