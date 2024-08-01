using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class PowerUp_004 : PowerUp
{
    [SerializeField] public ToxinSplash toxicZonePrefab;
    [SerializeField] public Transform ingameCell;
    [SerializeField] public float spawnRaidus = 10f;
    [SerializeField] public float spawnInterval = 5.0f;


    protected override void Start()
    {
        base.Start();
        timeCharge = spawnInterval;
        toxicZonePrefab = Resources.Load<ToxinSplash>("Prefab/Bullet Prefabs/ToxinArea");
        ingameCell = GetComponentInParent<Transform>();
        Debug.Log(ingameCell);
        Debug.Log(toxicZonePrefab); 
    }
    protected override void OnFire()
    {
        Vector3 spawnPosiotion = ingameCell.position + (Vector3)Random.insideUnitCircle * spawnRaidus;
        ToxinSplash toxinSplash = LeanPool.Spawn(toxicZonePrefab, spawnPosiotion, Quaternion.identity);
        toxinSplash = toxicZonePrefab.GetComponent<ToxinSplash>();
        toxinSplash.targetScale = scanRadius;
        toxinSplash.damage = modifiedDamage;
    }
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
        PowerUpData_ToxinPlash powerUPdata = (PowerUpData_ToxinPlash)GameManager.Instance.listPlayerPowerUpDatas.Find(x => x.id == id);
        multishot = powerUPdata.toxinPlashUpgrades[this.lv].plashAmount;
        modifiedDamage = powerUPdata.toxinPlashUpgrades[this.lv].damage;
        scanRadius = powerUPdata.toxinPlashUpgrades[this.lv].raidus;
    }
}
