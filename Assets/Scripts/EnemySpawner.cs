using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    private float spawnTime;
    [SerializeField] private float spawnTimer = 4.0f;
    void Start()
    {
        SpawnEnemy();
        spawnTime = spawnTimer;
    }

    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0.0f)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Instantiate(Enemies[Random.Range(0, 3)], new Vector3(Random.Range(-3.0f, 3.0f), 7.8f, 0), Quaternion.identity);
        spawnTime = spawnTimer;
    }
}
