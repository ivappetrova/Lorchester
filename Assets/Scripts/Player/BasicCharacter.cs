using UnityEngine;

namespace Player
{
    public class BasicCharacter : MonoBehaviour
    {
        protected MovementBehaviour MovementBehaviour;

        protected virtual void Awake()
        {
            GetComponent<AttackBehaviour>();
            MovementBehaviour = GetComponent<MovementBehaviour>();
            
        }
    }
}