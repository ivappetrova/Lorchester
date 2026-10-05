using Player;
using UnityEngine;
using Utils;

namespace GameElements.Obstacles
{
    public class ThornObstacle : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint; 
        [SerializeField] private CharacterControlSwitcher _controlSwitcher; 
        [SerializeField] private Health _sharedHealth;

        private void OnTriggerEnter(Collider collision)
        {
            switch (collision.name)
            {
                case "Player":
                {
                    if (_sharedHealth)
                    {
                        Debug.Log("Player took dmg");
                        _sharedHealth.TakeDamage(1);
                    }
                    collision.transform.position = _spawnPoint.position;
                    break;
                }
                // Handle companion collision with thorns
                case "Companion" when _sharedHealth == null:
                    return;
                case "Companion":
                {
                    _sharedHealth.TakeDamage(1);
                    Debug.Log("Companion took dmg");                
                    if (_controlSwitcher)
                    {
                        _controlSwitcher.AutoSwitchToPlayer();
                    }

                    break;
                }
            }
        }
    }
}
