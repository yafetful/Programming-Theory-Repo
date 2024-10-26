using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    private float spawnInterval = 3.0f;
    private float minRangeX = -6f;
    private float maxRangeX = 8f;
    private float positionZ = 85.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
    }
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        float positionX = Random.Range(minRangeX, maxRangeX);
        Vector3 spawnPosition = new Vector3(positionX, 0, positionZ);
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, enemyPrefabs[enemyIndex].transform.rotation);
    }
}