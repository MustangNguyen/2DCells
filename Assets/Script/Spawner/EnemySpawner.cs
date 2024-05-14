using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.Mathematics;
using System.Linq;
using System;
using System.Threading;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private Transform enemyHoder;
    [SerializeField] private float respawnDistance = 20f;
    [SerializeField] private List<EnemyCell> listEnemyCell;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private CampaignLevel campaignLevel;
    // int array [weight][counter]
    [SerializeField] private int[][] enemiesCounter;
    [SerializeField] private int currentWave = 0;
    [SerializeField] private float waveDurationLeft = 0f;
    // 1 dictionary for each wave
    [SerializeField] private List<Dictionary<EnemyCell, int>> counterDict;
    private Transform spawnPos;
    private int totalWeight = 0;
    private bool isOnChangeWave = false;

    private int enemiesSpawned = 0;
    private void Start()
    {
        spawnPos = enemyHoder;
        Initialize();
        SpawnEnemies();
    }
    private void FixedUpdate()
    {
        enemiesSpawned = UpdateManager.Instance.enemiesCount;
        UpdateWave();


    }

    private void Initialize()
    {
        currentWave = 0;
        listEnemyCell = new();
        counterDict = new();
        listEnemyCell = Resources.LoadAll<EnemyCell>("Prefab/Enemy Prefabs").ToList();
        currentWave = 0;
        waveDurationLeft = campaignLevel.waves[0].waveDuration;
        enemiesCounter = new int[campaignLevel.waves[currentWave].listSpawn.Count][];
        for (int i = 0; i < campaignLevel.waves[currentWave].listSpawn.Count; i++)
        {
            enemiesCounter[i] = new int[2];
            enemiesCounter[i][0] = campaignLevel.waves[currentWave].listSpawn[i].weight;
            enemiesCounter[i][1] = 0;
            // counterDict[campaignLevel.waves[currentWave].listSpawn[i].enemyCells] = i;
            counterDict.Add(new Dictionary<EnemyCell, int>());
            totalWeight += enemiesCounter[i][0];
        }
    }
    private void UpdateWave()
    {
        if (waveDurationLeft <= 0)
        {
            isOnChangeWave = true;
            currentWave++;
            if (campaignLevel.waves.Count > currentWave)
            {
                totalWeight = 0;
                counterDict.Add(new Dictionary<EnemyCell, int>());
                waveDurationLeft = campaignLevel.waves[currentWave].waveDuration;
                enemiesCounter = new int[campaignLevel.waves[currentWave].listSpawn.Count][];
                for (int i = 0; i < campaignLevel.waves[currentWave].listSpawn.Count; i++)
                {
                    enemiesCounter[i] = new int[2];
                    enemiesCounter[i][1] = campaignLevel.waves[currentWave].listSpawn[i].weight;
                    totalWeight += enemiesCounter[i][1];
                    // counterDict[campaignLevel.waves[currentWave].listSpawn[i].enemyCells] = i;
                }

            }
            else
            {
                currentWave--;
                waveDurationLeft = campaignLevel.waves[currentWave].waveDuration;
            }
            isOnChangeWave = false;
        }
        waveDurationLeft -= Time.fixedDeltaTime;
    }
    private void SpawnEnemies()
    {
        //StartCoroutine(IESpawnSchedule());
        StartCoroutine(IESpawnByLevelScript());
    }
    protected Vector3 SetTargetCyclePos(float spawnRadius, Vector3 playerPos)
    {
        float randomAngle = UnityEngine.Random.value;
        float angleInDegrees = randomAngle * 360;
        float angleInRadians = Mathf.Deg2Rad * angleInDegrees;
        float spawnX = playerPos.x + spawnRadius * Mathf.Cos(angleInRadians);
        float spawnY = playerPos.y + spawnRadius * Mathf.Sin(angleInRadians);
        Vector3 position = new Vector3(spawnX, spawnY, 0);
        return position;
    }
    private IEnumerator IESpawnSchedule()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if (enemiesSpawned < GameManager.Instance.maximumEnemies - 1)
                LeanPool.Spawn(listEnemyCell[0], SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
            yield return new WaitForSeconds(spawnTime);
            if (enemiesSpawned < GameManager.Instance.maximumEnemies - 1)
                LeanPool.Spawn(listEnemyCell[1], SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
        }
    }
    private IEnumerator IESpawnByLevelScript()
    {
        EnemyCell enemyCell;
        while (true)
        {
            if (isOnChangeWave) yield return new WaitForFixedUpdate();
            //Debug.Log("spawning");
            if (counterDict[currentWave].Count == 0)
            {
                enemyCell = LeanPool.Spawn(campaignLevel.waves[currentWave].listSpawn[0].enemyCells, SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
                counterDict[currentWave][enemyCell] = 0;
                enemyCell.wave = currentWave;
                enemiesCounter[0][1]++;
            }
            for (int i = 0; i < campaignLevel.waves[currentWave].listSpawn.Count; i++)
            {
                yield return new WaitForSeconds(spawnTime);
                if (enemiesSpawned < GameManager.Instance.maximumEnemies - 1)
                {
                    if ((float)enemiesCounter[i][1] / counterDict[currentWave].Count <= (float)enemiesCounter[i][0] / totalWeight)
                    {
                        enemyCell = LeanPool.Spawn(campaignLevel.waves[currentWave].listSpawn[i].enemyCells, SetTargetCyclePos(spawnRadius, playerPosition.position), quaternion.identity, enemyHoder);
                        counterDict[currentWave][enemyCell] = i;
                        enemyCell.wave = currentWave;
                        enemiesCounter[i][1]++;
                        Debug.Log(enemiesCounter[i][0] + " : " + enemiesCounter[i][1]);

                    }
                    // foreach(KeyValuePair<EnemyCell,int> pair in counterDict){
                    //     Debug.Log($"Khóa: {pair.Key}, Giá trị: {pair.Value}");
                    // }
                }
            }
        }
    }
    public void OnEnemyDestroy(EnemyCell enemyCell)
    {
        if (enemyCell.wave == currentWave)
        {
            int index = counterDict[currentWave][enemyCell];
            // Debug.Log(index);
            enemiesCounter[index][1]--;
        }
    }
}