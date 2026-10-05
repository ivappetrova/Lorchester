using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Obstacles
{
    public class ZoneController : MonoBehaviour
    {
        [SerializeField] private List<ShootingObstacle> _shooters = new List<ShootingObstacle>();

        public void ActivateZone()
        {
            Debug.Log($"[{name}] ActivateZone - {_shooters.Count} shooters in list");
            foreach (var shooter in _shooters)
            {
                if (shooter)
                {
                    shooter.SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"[{name}] Null entry in shooters list");
                }
            }
        }

        public void DeactivateZone()
        {
            Debug.Log($"[{name}] DeactivateZone - {_shooters.Count} shooters in list");
            foreach (var shooter in _shooters)
            {
                if (shooter)
                {
                    shooter.SetActive(false);
                }
            }
        }
    }
}