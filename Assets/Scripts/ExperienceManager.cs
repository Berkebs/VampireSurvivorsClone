using UnityEngine;
using UnityEngine.Events;

namespace VampireSurvivorsClone
{
    public class ExperienceManager : MonoBehaviour
    {
        public static ExperienceManager Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentExperience = 0;
        [SerializeField] private int baseExperienceReq = 100;
        [SerializeField] private float experienceGrowthFactor = 1.2f;

        [Header("Events")]
        public UnityEvent<int, int> OnExperienceChange; // Current, Max
        public UnityEvent<int> OnLevelUp; // New Level

        public int MaxExperience => Mathf.RoundToInt(baseExperienceReq * Mathf.Pow(experienceGrowthFactor, currentLevel - 1));

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            // Init UI
            OnExperienceChange?.Invoke(currentExperience, MaxExperience);
        }

        public void AddExperience(int amount)
        {
            currentExperience += amount;
            CheckLevelUp();
            OnExperienceChange?.Invoke(currentExperience, MaxExperience);
        }

        private void CheckLevelUp()
        {
            int req = MaxExperience;
            while (currentExperience >= req)
            {
                currentExperience -= req;
                currentLevel++;
                OnLevelUp?.Invoke(currentLevel);
                
                // Recalculate req for next loop if multiple level ups happen (rare but possible)
                req = MaxExperience;
            }
        }
    }
}
