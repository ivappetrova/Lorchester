using Movables;
using UnityEngine;

namespace GameElements
{
    public class Destructible : MonoBehaviour
    {
        [SerializeField] private Transform _keySpawnLocation;
        [SerializeField] private Door _targetDoor;
        
        [SerializeField] private GameObject _keyPrefab;
        [SerializeField] private GameObject _breakEffectPrefab;

        public void Break()
        {
            if (_breakEffectPrefab)
            {
                Instantiate(_breakEffectPrefab, transform.position, Quaternion.identity);
            }

            if (_keyPrefab && _keySpawnLocation)
            {
                var spawnedKey = Instantiate(_keyPrefab, _keySpawnLocation.position, Quaternion.identity);

                // Set the target door for the spawned key
                var keyScript = spawnedKey.GetComponent<Key>();
                if (keyScript && _targetDoor)
                {
                    keyScript.SetTargetDoor(_targetDoor);
                }
                else
                {
                    Debug.Log("Key or Target Door is missing!");
                }
            }
            Destroy(gameObject);
        }
    }
}
