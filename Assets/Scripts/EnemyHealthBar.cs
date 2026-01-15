using UnityEngine;

namespace VampireSurvivorsClone
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Transform barFill;
        [SerializeField] private SpriteRenderer barSpriteRenderer;
        
        [Header("Appearance")]
        [SerializeField] private Gradient colorGradient;
        [Tooltip("Hide bar if health is full?")]
        [SerializeField] private bool hideOnFull = true;

        private void Start()
        {
            // Auto-find sprite renderer if barFill is set but renderer isn't
            if (barFill != null && barSpriteRenderer == null)
            {
                barSpriteRenderer = barFill.GetComponent<SpriteRenderer>();
            }
        }

        public void UpdateBar(float current, float max)
        {
            if (barFill == null) return;

            float percent = Mathf.Clamp01(current / max);
            
            // Adjust X scale
            Vector3 scale = barFill.localScale;
            scale.x = percent;
            barFill.localScale = scale;

            // Adjust Color
            if (barSpriteRenderer != null)
            {
                // Evaluate gradient (1f = Right side of gradient, 0f = Left side)
                // Usually Gradient is setups as: Right=Green(High), Left=Red(Low)
                // Depending on editor setup, you might need to check how user defines it.
                // Default Unity Gradient goes 0..1. 
                // If percent is 1 (Full), we want color at time 1.
                
                // Note: If no gradient is set, handle gracefully? 
                // Unity makes a default gradient usually black-white. 
                // Let's assume user will set it.
                
                // Check if gradient is initialized (Unity serialized gradients are not null usually)
                barSpriteRenderer.color = colorGradient.Evaluate(percent);
            }

            // Optional: Hide if full
            if (hideOnFull)
            {
                // Only show if damaged
                bool shouldShow = percent < 1f;
                if (gameObject.activeSelf != shouldShow) 
                {
                    gameObject.SetActive(shouldShow);
                }
            }
        }
    }
}
