using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using TMPro;
using Unity.VisualScripting;
using System;

public class EnemyCell : CellsBase
{
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [Space(10)]
    [Header("UI")]
    [SerializeField] protected string enemyId;
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected int index;
    [SerializeField] protected Equipment equipment;
    [SerializeField] protected StateMachine stateMachine;
    protected override void Start()
    {
        base.Start();
        AddProperties();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        
        UpdateManager.Instance.AddCellToPool(this);
        index = UpdateManager.Instance.poolIndex;
    }
    private void Reset() {
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
    }
    public void CellUpdate(){
        healthText.text = healPoint.ToString();
    }
    public void CellFixedUpdate()
    {
        healthBar.value = (float)healPoint / (float)maxHealth;
        StateMachineMonitor();
        movement();
        if (healPoint <= 0)
        {
            OnDead();
        }
    }
    public void movement()
    {
        Vector3 moveDirection = (GameManager.Instance.playerPosition.transform.position - transform.position).normalized;
        //rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        rigidbody2d.velocity = moveDirection * moveSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        string damageSource = other.gameObject.tag;
        (int, int) damageTaken;
        (int, int) finalDamage;
        switch (damageSource)
        {
            case "Bullet1":
                finalDamage = DamageCalculator.Instance.DamageManager(true);
                damageTaken = DamageCalculator.Instance.DamageTake(currentArmor, BioArmorCalculating(), finalDamage.Item1, GameManager.Instance.cellGun1.bulletPrefab.Elements);
                healPoint -= damageTaken.Item1;
                currentArmor.armorPoint -= damageTaken.Item2;
                EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, finalDamage.Item2, transform);
                break;
            case "Bullet2":
                finalDamage = DamageCalculator.Instance.DamageManager(false);
                damageTaken = DamageCalculator.Instance.DamageTake(currentArmor, BioArmorCalculating() , finalDamage.Item1, GameManager.Instance.cellGun2.bulletPrefab.Elements);
                healPoint -= damageTaken.Item1;
                currentArmor.armorPoint -= damageTaken.Item2;
                EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, finalDamage.Item2, transform);
                break;
            default:
                healPoint -= 0;
                break;
        }
        if (currentArmor.armorPoint <= baseCellArmor.armorPoint)
        {
            currentArmor.armorPoint = baseCellArmor.armorPoint;
        }
        //Debug.Log("fix armor: "+baseCellArmor.armorPoint);
    }
    protected void StateMachineMonitor(){
        if(rigidbody2d.velocity==Vector2.zero){
            stateMachine.ChangeState(new EnemyStateIdle(this));
        }
        else if(rigidbody2d.velocity!=Vector2.zero){
            stateMachine.ChangeState(new EnemyStateMove(this));
        }
    }
    protected void Init(){
        foreach(var enemy in DataManager.Instance.Data.listEnemies){
            if(enemyId == enemy.enemyId){
                
            }
        }
    }
    protected void AddProperties()
    {
        if(DataManager.Instance.Data.listEnemies.Exists(x=> x.enemyId == this.enemyId))
        {
            EnemyCellOOP enemyCellOOP = DataManager.Instance.Data.listEnemies.Find(x => x.enemyId == this.enemyId);
            this.maxHealth = enemyCellOOP.hp;
            this.healPoint = this.maxHealth;
            this.moveSpeed = enemyCellOOP.moveSpeed;
            this.baseCellArmor.armorPoint = enemyCellOOP.cellProtection.armorPoint;
            this.faction = enemyCellOOP.faction;
        }
    }
    protected override void OnDead()
    {
        base.OnDead();
        UpdateManager.Instance.RemoveCellFromPool(index);
        LeanPool.Despawn(gameObject);
    }
}
[Serializable]
public class EnemyCellOOP{
    public string enemyId;
    public string enemyName;
    public int hp;
    public int mp;
    public CellProtection cellProtection;
    public float moveSpeed;
    public string abilityId;
    public Faction faction;
    public Equipment equipment;
    public EnemyCellOOP(){
        cellProtection =new CellProtection();
    }
}
[Serializable]
public enum Equipment{
    None,
    Melee,
    Range
}