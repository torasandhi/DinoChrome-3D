using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> obstacleSpawnPositions = new List<Transform>();
    [SerializeField] private int maxPoolSize = 10;
    [SerializeField] private float obstacleLifetime = 10f;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;

    private List<GameObject> obstaclePool = new List<GameObject>();

    private void Start()
    {
        InitializePool();
        StartCoroutine(SpawnRoutine());
    }

    private void InitializePool()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject prefab = obstaclePrefabs[i % obstaclePrefabs.Count];
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obstaclePool.Add(obj);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPattern();
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator DespawnRoutine(GameObject activeObstacle)
    {
        yield return new WaitForSeconds(obstacleLifetime);

        if (activeObstacle.activeInHierarchy)
        {
            activeObstacle.SetActive(false);
            obstaclePool.Add(activeObstacle);
            Debug.Log("Despawned");
        }
    }

    private void SpawnPattern()
    {
        // Define possible spawn patterns
        List<int[]> patterns = new List<int[]>
        {
            new int[] { 0 },           // left
            new int[] { 1 },           // middle
            new int[] { 2 },           // right
            new int[] { 0, 1 },        // left + middle
            new int[] { 0, 2 },        // left + right
            new int[] { 1, 2 },        // middle + right
            new int[] { 0, 1, 2 }      // all three

        };

        int[] chosenPattern = patterns[Random.Range(0, patterns.Count)];

        foreach (int laneIndex in chosenPattern)
        {
            GameObject obstacle = GetPooledObstacle();
            if (obstacle != null)
            {
                Rigidbody rb = obstacle.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                }

                obstacle.transform.position = obstacleSpawnPositions[laneIndex].position;
                obstacle.transform.rotation = Random.rotation;
                obstacle.SetActive(true);
                StartCoroutine(DespawnRoutine(obstacle));
            }
        }
    }

    private GameObject GetPooledObstacle()
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Optional: expand pool if needed (careful for performance)
        if (obstaclePool.Count < maxPoolSize * 2)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obstaclePool.Add(obj);
            return obj;
        }

        return null;
    }
}
