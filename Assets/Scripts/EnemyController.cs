using UnityEngine;

namespace VampireSurvivorsClone
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("Stats")]
        [SerializeField] private float maxHealth = 20f;
        [SerializeField] private float moveSpeed = 2f;
        
        private Transform target;
        [Header("UI")]
        [SerializeField] private EnemyHealthBar healthBar;

        [Header("Debug")]
        [SerializeField] private float currentHealth;
        
        private Rigidbody2D rb; // Restored
        private SpriteRenderer spriteRenderer;
        private Color originalColor;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            // Store the original color (e.g., if user set it to Red or White)
            if (spriteRenderer) originalColor = spriteRenderer.color;
            
            currentHealth = maxHealth;
        }

        private void Start()
        {
            // Simple way to find player for prototype
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) target = player.transform;
            
            // Initialize bar
            if (healthBar) healthBar.UpdateBar(currentHealth, maxHealth);
        }

        private void FixedUpdate()
        {
            if (target)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.linearVelocity = direction * moveSpeed;
                
                // Optional facing direction
                if(direction.x != 0)
                     transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            
            // Update Bar
            if (healthBar) healthBar.UpdateBar(currentHealth, maxHealth);
            
            // 1. Visual Feedback: Flash White instantly
            StartCoroutine(FlashEffect());

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private System.Collections.IEnumerator FlashEffect()
        {
            if (!spriteRenderer) yield break;

            // Flash visual
            spriteRenderer.color = Color.white; // Or a flash color
            // Slight scale up for impact
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 1.2f;

            yield return new WaitForSeconds(0.1f);

            // Return to normal
            // Calculate health tint: As health drops, become darker (redder)
            float healthPercent = Mathf.Clamp01(currentHealth / maxHealth);
            // Lerp from Original Color to Dark Gray/Red based on health
            Color healthColor = Color.Lerp(Color.grey, originalColor, healthPercent); 
            
            spriteRenderer.color = healthColor;
            transform.localScale = originalScale;
        }

        public void ResetEnemy()
        {
            currentHealth = maxHealth;
            if (spriteRenderer) spriteRenderer.color = originalColor;
            transform.localScale = Vector3.one; // Reset scale just in case
            
            if (healthBar) healthBar.UpdateBar(currentHealth, maxHealth);
        }

        [Header("Loot")]
        [SerializeField] private GameObject gemPrefab;

        private void Die()
        {
            // Drop Gem
            if (gemPrefab != null)
            {
                Instantiate(gemPrefab, transform.position, Quaternion.identity);
            }

            var spawner = FindFirstObjectByType<EnemySpawner>(); 
            if (spawner)
            {
                spawner.ReturnToPool(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
