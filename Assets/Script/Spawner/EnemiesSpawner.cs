using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.Mathematics;

public class EnemiesSpawner : Spawner
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float spawnRadius = 15f;

    private int poolIndex = 0;
    protected override void Start() {
        base.Start();
        SpawnEnemies();
    }
    private void Update()
    {
        poolIndex = UpdateManager.Instance.poolIndex;
    }
    protected override void SpawnEnemies()
    {
        base.SpawnEnemies();
        StartCoroutine(IESpawnSchedule());
    }
    private IEnumerator IESpawnSchedule()
    {
        while (poolIndex < GameManager.Instance.maximumEnemies - 1)
        {
            yield return new WaitForSeconds(spawnTime);
            LeanPool.Spawn(enemy, SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
        }
    }
}
