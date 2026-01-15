using UnityEngine;

namespace VampireSurvivorsClone
{
    public class ExperienceGem : MonoBehaviour
    {
        [SerializeField] private int xpAmount = 10;
        [SerializeField] private float moveSpeed = 10f;
        
        private Transform target;
        private bool isCollected = false;

        public void Collect(Transform collector)
        {
            if (isCollected) return;
            isCollected = true;
            target = collector;
        }

        private void Update()
        {
            if (target != null)
            {
                // Move towards player
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                
                // Check distance
                if (Vector3.Distance(transform.position, target.position) < 0.5f)
                {
                    OnReachPlayer();
                }
            }
        }

        private void OnReachPlayer()
        {
            if (ExperienceManager.Instance)
            {
                ExperienceManager.Instance.AddExperience(xpAmount);
            }
            
            // Return to pool or destroy
            // Assuming we will add GemSpawner/Pool later. For now Destroy.
            Destroy(gameObject);
        }
    }
}
