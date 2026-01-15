using UnityEngine;

namespace VampireSurvivorsClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerCollector : MonoBehaviour
    {
        [SerializeField] private float radius = 3f;
        [SerializeField] private LayerMask gemLayer;

        private CircleCollider2D pickupArea;

        private void Awake()
        {
            pickupArea = GetComponent<CircleCollider2D>();
            pickupArea.isTrigger = true;
            pickupArea.radius = radius;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if object is in the target layer
            // (1 << other.gameObject.layer) creates a bitmask for the object's layer
            // 'value & bitmask != 0' checks if that bit is set in the mask
            if ((gemLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                var gem = other.GetComponent<ExperienceGem>();
                if (gem != null)
                {
                    gem.Collect(transform);
                }
            }
        }
        
        // Optional: Draw Gizmo
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
