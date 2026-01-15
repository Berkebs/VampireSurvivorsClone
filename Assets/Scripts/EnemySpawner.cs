using System.Collections.Generic;
using UnityEngine;

namespace VampireSurvivorsClone
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnRate = 1f; // Enemies per second
        [SerializeField] private float spawnRadius = 10f; // Distance from player to spawn
        [Tooltip("Extra buffer to ensure spawning is off-screen")]
        [SerializeField] private float offScreenBuffer = 2f;

        [Header("Pooling")]
        [SerializeField] private int poolSize = 50;

        private Transform playerTransform;
        private float nextSpawnTime;
        private Queue<GameObject> enemyPool;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) playerTransform = player.transform;

            InitializePool();
        }

        private void InitializePool()
        {
            enemyPool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                CreateNewEnemyForPool();
            }
        }

        private GameObject CreateNewEnemyForPool()
        {
            GameObject obj = Instantiate(enemyPrefab, transform);
            obj.SetActive(false);
            enemyPool.Enqueue(obj);
            return obj;
        }

        private void Update()
        {
            if (!playerTransform) return;

            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + (1f / spawnRate);
            }
        }

        private void SpawnEnemy()
        {
            if (enemyPool.Count == 0)
            {
                // Optional: Expand pool if needed, or just skip spawn
                CreateNewEnemyForPool();
            }

            GameObject enemy = enemyPool.Dequeue(); 

            // If the enemy is still active (somehow wasn't despawned properly), skip it or reset it.
            // But usually, when they die, they should return to pool. 
            // For this simple prototype, let's assume dequeue gives us an available one 
            // OR we need to find an inactive one if we shared the list. 
            // Queue approach assumes we return them to queue when they die.
            
            // Wait! The Queue approach works best if we return them. 
            // We need a way to return enemies to the pool when they 'die'.
            // For now, let's just make sure it's inactive before re-using
            
            enemy.SetActive(true);
            
            // Position calculation: Random point on circle edge
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            Vector3 spawnPos = playerTransform.position + (Vector3)(randomDir * (spawnRadius + offScreenBuffer));
            enemy.transform.position = spawnPos;

            // Reset enemy state if needed (Health, etc.)
            // We can do this by GetComponent or broadcasting a message
            var enemyCtrl = enemy.GetComponent<EnemyController>();
            if (enemyCtrl)
            {
                // We need to add a Reset/Revive method to EnemyController later.
                // For now, just re-enabling might not enough if health is 0.
                enemyCtrl.ResetEnemy(); 
            }
        }

        public void ReturnToPool(GameObject enemy)
        {
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }
}
