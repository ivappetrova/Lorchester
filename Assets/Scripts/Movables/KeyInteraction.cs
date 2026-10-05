using UnityEngine;

namespace Movables
{
    public class KeyInteraction : MonoBehaviour
    {
        [SerializeField] private Key _currentKey; 
        void Update()
        {
            if (_currentKey && !_currentKey.IsCollected)
            {
              //  Debug.Log("In range of the key.");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
             
                _currentKey = other.GetComponent<Key>();

                if (_currentKey && !_currentKey.IsCollected)
                {
                    _currentKey.CollectKey(); 
                    Debug.Log("Key collected!");
                }
                else
                {
                   // Debug.LogWarning("Key is either already collected or not found.");
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Key"))
            { 
                _currentKey = null;
            }
        }
    }
}