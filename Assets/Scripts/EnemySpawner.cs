using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnHeightMin = 1f;
    [SerializeField] private float spawnHeightMax = 4f;
    [SerializeField] private Transform spawnPoint;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.y = Random.Range(spawnHeightMin, spawnHeightMax);

        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
    }
}
