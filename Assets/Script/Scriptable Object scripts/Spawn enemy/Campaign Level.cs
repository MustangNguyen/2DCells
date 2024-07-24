using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Campaign Level", menuName = "Scriptable Objects/Spawn/Campaign Level", order = 2)]
public class CampaignLevel : ScriptableObject
{
    public string nodeId;
    public List<WaveSpawn> waves =new();
    public List<BossSchadule> listEnemyBoss;
    private void OnEnable() {
        foreach(var boss in listEnemyBoss){
            boss.isSpawned = false;
        }
    }
}
