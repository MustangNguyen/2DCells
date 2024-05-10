using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] protected float spawnTime = 1f;
    [SerializeField] protected Transform enemyHoder;
    protected Transform spawnPos;
    protected virtual void Start(){
        GameObject gameObject = new GameObject();
        spawnPos = enemyHoder;
    }
    protected virtual void SpawnEnemies(){

    }
    protected Vector3 SetTargetCyclePos(float spawnRadius,Vector3 playerPos)
    {
        float randomAngle = Random.value;
        float angleInDegrees = randomAngle * 360;
        float angleInRadians = Mathf.Deg2Rad * angleInDegrees;
        float spawnX = playerPos.x + spawnRadius * Mathf.Cos(angleInRadians);
        float spawnY = playerPos.y + spawnRadius * Mathf.Sin(angleInRadians);
        Vector3 position = new Vector3(spawnX, spawnY, 0);
        return position;
    }

}
