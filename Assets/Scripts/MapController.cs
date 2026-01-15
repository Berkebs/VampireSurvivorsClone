using UnityEngine;

namespace VampireSurvivorsClone
{
    public class MapController : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The prefab or object that represents a single map chunk.")]
        [SerializeField] private GameObject mapChunkPrefab;
        
        [Tooltip("The player to follow. If null, will try to find object with 'Player' tag.")]
        [SerializeField] private Transform playerTransform;
        
        [Tooltip("Size of one square chunk (e.g., 20 if sprite is 20x20 units).")]
        [SerializeField] private float chunkSize = 20f;

        [Tooltip("How many chunks to keep around the player (Radius). 1 means 3x3 grid.")]
        [SerializeField] private int viewRadius = 1;

        private GameObject[,] chunks;
        private Vector2Int currentChunkCoord;

        private void Start()
        {
            if (!playerTransform)
            {
                var p = GameObject.FindGameObjectWithTag("Player");
                if (p) playerTransform = p.transform;
                else Debug.LogWarning("MapController: Player not found!");
            }

            InitialSpawn();
        }

        private void Update()
        {
            if (!playerTransform) return;

            Vector2Int playerChunkCoord = GetChunkCoordFromPosition(playerTransform.position);

            if (playerChunkCoord != currentChunkCoord)
            {
                UpdateChunks(playerChunkCoord);
                currentChunkCoord = playerChunkCoord;
            }
        }

        private void InitialSpawn()
        {
            // Calculate grid size based on radius (e.g., radius 1 -> 3x3)
            int diameter = viewRadius * 2 + 1;
            chunks = new GameObject[diameter, diameter];
            currentChunkCoord = GetChunkCoordFromPosition(playerTransform.position);

            for (int x = -viewRadius; x <= viewRadius; x++)
            {
                for (int y = -viewRadius; y <= viewRadius; y++)
                {
                    SpawnChunk(currentChunkCoord.x + x, currentChunkCoord.y + y, x + viewRadius, y + viewRadius);
                }
            }
        }

        private void UpdateChunks(Vector2Int newCenterCoord)
        {
            // This is a simple implementation: 
            // In a more optimized version, we would recycle chunks that are far away 
            // and move them to the new positions.
            // For now, we will just move the existing pool's transforms to match the new grid.
            // But implementing a proper "wrap around" logic for the 2D array is tricky.
            
            // Simpler robust approach for prototype:
            // Iterate through the target grid positions. 
            // If we have a chunk there, keep it. 
            // If we don't, move a far-away chunk there.
            
            // To keep it very simple for Step 1: 
            // We'll just re-position all chunks relative to the player's new center.
            // NOTE: This might cause texture popping if texturing isn't world-aligned or identical.
            // Since we assume identical tiling sprites, it's fine.

            int diameter = chunks.GetLength(0);
            
            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    GameObject chunk = chunks[x, y];
                    
                    // Calculate logical offset of this array slot from the center slot
                    // e.g. if radius is 1 (3x3), accessing [0,0] means offset (-1, -1)
                    int offsetX = x - viewRadius;
                    int offsetY = y - viewRadius;

                    Vector2Int targetGridCoord = newCenterCoord + new Vector2Int(offsetX, offsetY);
                    Vector3 newPos = new Vector3(targetGridCoord.x * chunkSize, targetGridCoord.y * chunkSize, 0);

                    chunk.transform.position = newPos;
                }
            }
        }

        private void SpawnChunk(int x, int y, int arrayX, int arrayY)
        {
            Vector3 pos = new Vector3(x * chunkSize, y * chunkSize, 0);
            GameObject chunk = Instantiate(mapChunkPrefab, pos, Quaternion.identity, transform);
            chunk.name = $"Chunk_{x}_{y}";
            chunks[arrayX, arrayY] = chunk;
        }

        private Vector2Int GetChunkCoordFromPosition(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x / chunkSize);
            int y = Mathf.RoundToInt(position.y / chunkSize);
            return new Vector2Int(x, y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            // Draw the chunk that the player is currently in (simulated)
            if (playerTransform)
            {
                var center = GetChunkCoordFromPosition(playerTransform.position);
                Vector3 centerPos = new Vector3(center.x * chunkSize, center.y * chunkSize, 0);
                Gizmos.DrawWireCube(centerPos, new Vector3(chunkSize, chunkSize, 0.1f));
            }

            // Draw the spawn radius
            if (Application.isPlaying && chunks != null)
            {
                Gizmos.color = Color.yellow;
                foreach (var chunk in chunks)
                {
                    if (chunk) Gizmos.DrawWireCube(chunk.transform.position, new Vector3(chunkSize, chunkSize, 0.1f));
                }
            }
        }
    }
}
