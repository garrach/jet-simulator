using UnityEngine;

public class InfiniteLevelGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab for obstacles
    public float spawnDistance = 20f; // Distance between obstacle spawns
    public int poolSize = 10; // Number of obstacles in the pool
    public Transform playerTransform;

    private GameObject[] obstaclePool;
    private float nextSpawnPosition = 0f;

    void Start()
    {
        InitializeObstaclePool();
    }

    void Update()
    {
        // Check if the player is approaching the next spawn position
        if (playerTransform.position.z > nextSpawnPosition)
        {
            SpawnObstacle();
        }
    }

    void InitializeObstaclePool()
    {
        // Initialize the obstacle pool
        obstaclePool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            obstaclePool[i] = Instantiate(obstaclePrefab, Vector3.back * 1000f, Quaternion.identity);
            obstaclePool[i].SetActive(false);
        }
    }

    void SpawnObstacle()
    {
        // Get an obstacle from the pool and set its position
        GameObject obstacle = GetNextObstacle();
        obstacle.transform.position = new Vector3(Random.Range(-5f, 5f), 0f, nextSpawnPosition);
        obstacle.SetActive(true);

        // Update the next spawn position
        nextSpawnPosition += spawnDistance;
    }

    GameObject GetNextObstacle()
    {
        // Find the next inactive obstacle in the pool
        foreach (var obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }

        // If no inactive obstacle is found, expand the pool
        GameObject newObstacle = Instantiate(obstaclePrefab, Vector3.back * 1000f, Quaternion.identity);
        obstaclePool = ExpandArray(obstaclePool, newObstacle);

        return newObstacle;
    }

    GameObject[] ExpandArray(GameObject[] array, GameObject newItem)
    {
        // Expand the array and add a new item
        GameObject[] newArray = new GameObject[array.Length + 1];
        array.CopyTo(newArray, 0);
        newArray[array.Length] = newItem;
        return newArray;
    }
}
