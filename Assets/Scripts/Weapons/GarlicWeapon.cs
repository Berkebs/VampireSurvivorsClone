using System.Collections.Generic;
using UnityEngine;

namespace VampireSurvivorsClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GarlicWeapon : MonoBehaviour
    {
        [Header("Weapon Stats")]
        [SerializeField] private float damage = 5f;
        [SerializeField] private float hitRate = 1f; // Seconds between hits
        [SerializeField] private float radius = 1.5f;

        private CircleCollider2D hitArea;
        private List<IDamageable> targetsInRange = new List<IDamageable>();
        private float nextHitTime;

        private void Awake()
        {
            hitArea = GetComponent<CircleCollider2D>();
            hitArea.isTrigger = true;
            hitArea.radius = radius;
        }

        private void Update()
        {
            if (Time.time >= nextHitTime)
            {
                ApplyDamage();
                nextHitTime = Time.time + hitRate;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Assuming enemies are on a specific layer or have a specific tag.
            // Using interface check is robust.
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null && !targetsInRange.Contains(damageable))
            {
                targetsInRange.Add(damageable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                targetsInRange.Remove(damageable);
            }
        }

        private void ApplyDamage()
        {
            // Remove null objects (destroyed enemies) safely
            targetsInRange.RemoveAll(x => x == null || x.Equals(null));

            // Iterate over a COPY of the list because TakeDamage -> Die -> SetActive(false) 
            // causes OnTriggerExit2D to fire immediately, modifying the original targetsInRange list
            // while we are still looping through it.
            var targetsCopy = new List<IDamageable>(targetsInRange);

            foreach (var target in targetsCopy)
            {
                // Double check if the target is still in the original list (valid)
                // (Optional, but safe if multiple things happen)
                if (target != null && targetsInRange.Contains(target))
                {
                    target.TakeDamage(damage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
