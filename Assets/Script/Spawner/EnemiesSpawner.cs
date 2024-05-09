using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.Mathematics;

public class EnemiesSpawner : Spawner
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float spawnRadius = 15f;

    private int enemiesSpawned = 0;
    protected override void Start() {
        base.Start();
        SpawnEnemies();
    }
    private void Update()
    {
        enemiesSpawned = UpdateManager.Instance.enemiesCount;
    }
    protected override void SpawnEnemies()
    {
        base.SpawnEnemies();
        StartCoroutine(IESpawnSchedule());
    }
    private IEnumerator IESpawnSchedule()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if(enemiesSpawned < GameManager.Instance.maximumEnemies - 1)
                LeanPool.Spawn(enemy, SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
            yield return new WaitForSeconds(spawnTime);
            if (enemiesSpawned < GameManager.Instance.maximumEnemies - 1)
                LeanPool.Spawn(enemy2, SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
        }
    }
}
