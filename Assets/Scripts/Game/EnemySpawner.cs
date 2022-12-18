using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    private ScoreManager scoreManager;
    private float spawnTime;
    [SerializeField] private float spawnTimer = 3.0f;
    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();

        spawnTime = spawnTimer;
        SpawnMeteorites();
    }

    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0.0f && scoreManager.score <= 4)
            SpawnMeteorites();
        
        if (spawnTime <= 0.0f && scoreManager.score > 4 && scoreManager.score <= 8)
            SpawnEnemies();

        if (spawnTime <= 0.0f && scoreManager.score > 8)
            SpawnMoreEnemies();
    }

    private void SpawnMeteorites()
    {
        Instantiate(Enemies[3], new Vector3(Random.Range(-3.0f, 3.0f), 7.8f, 0), Quaternion.identity);
        spawnTime = spawnTimer;
    }
    private void SpawnEnemies()
    {
        Instantiate(Enemies[Random.Range(0, 4)], new Vector3(Random.Range(-3.0f, 3.0f), 7.8f, 0), Quaternion.identity);
        spawnTime = spawnTimer;
    }

    private void SpawnMoreEnemies()
    {
        Instantiate(Enemies[Random.Range(0, 3)], new Vector3(Random.Range(-3.0f, 3.0f), 7.8f, 0), Quaternion.identity);
        spawnTime = 1.5f;
    }
}
