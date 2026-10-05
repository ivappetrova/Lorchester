using UnityEngine;

namespace GameElements.Obstacles
{
    public enum BoundaryRole { Entrance, Exit }

    [RequireComponent(typeof(Collider))]
    public class ZoneBoundaryTrigger : MonoBehaviour
    {
        private ZoneController _zoneController;
        
        [SerializeField] private BoundaryRole _role;

        private void Awake()
        {
            _zoneController = GetComponentInParent<ZoneController>();

            Debug.Log($"[{name}] Awake - role={_role}, zoneController found: {_zoneController}");

            var colliderComponent = GetComponent<Collider>();
            if (!colliderComponent || colliderComponent.isTrigger) return;
            Debug.LogWarning($"{name}: Is Trigger is off - forcing it on.", this);
            colliderComponent.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!_zoneController) return;

            if (_role == BoundaryRole.Entrance)
            {
                _zoneController.ActivateZone();
            }
            else
            {
                _zoneController.DeactivateZone();
            }
        }
    }
}