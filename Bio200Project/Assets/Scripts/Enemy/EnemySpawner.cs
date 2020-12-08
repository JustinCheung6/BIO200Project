using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float startUpTimer = 5f;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private float spawnFrequency = 0.2f;
    [SerializeField] private int maxEnemies = 30;
    [SerializeField] private int waveCount = 5;

    private GameObject storage = null;
    private List<GameObject> enemies = new List<GameObject>();

    private bool startUp = false;
    private float timer = 0f;

    private void Start()
    {
        storage = Instantiate(new GameObject("EnemyStorage"), transform.parent);
    }

    private void Update()
    {
        if(!startUp && timer < startUpTimer)
            timer += Time.deltaTime;
        else if(!startUp) {
            startUp = true;
            timer = 0;
        }    
        else if (timer < spawnFrequency)
            timer += Time.deltaTime;
        else if(enemies.Count < maxEnemies)
            for(int i = 0; i < waveCount; i++)
                if(enemies.Count < maxEnemies) 
                {
                    timer = 0;
                    enemies.Add(SpawnEnemy());
                }
    }

    private GameObject SpawnEnemy()
    {
        Vector3 range = new Vector3((float)Random.Range(-5, 6), 0, 0);

        GameObject enemy = Instantiate(enemyPrefab, transform.position + range, Quaternion.identity, storage.transform);

        return enemy;
    }
}
