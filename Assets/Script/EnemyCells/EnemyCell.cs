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
    [Header("Enemy Properties")]
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected Collider2D collider2d;
    [SerializeField] protected int bodyDamage = 0;
    [Space(10)]
    [Header("UI")]
    [SerializeField] protected string enemyId;
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected SpriteRenderer model;
    [SerializeField] protected int index;
    [SerializeField] protected Equipment equipment;
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject destroyAnimation;
    [Space(10)]
    [Header("Wave")]
    [SerializeField] public int wave;
    protected override void Start()
    {
        base.Start();
        destroyAnimation.SetActive(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        AddProperties();
        UpdateManager.Instance.AddCellToPool(this);
        index = UpdateManager.Instance.poolIndex;
        Reset();
    }
    private void Reset() {
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
        model.color = new Color(1, 1, 1, 1);
        collider2d.enabled = true;
    }
    public void CellUpdate(){
        // healthText.text = healPoint.ToString();
        stateMachine.StateMachineUpdate();
        Spawner.Instance.Reposition(transform);
    }
    public void CellFixedUpdate()
    {
        healthBar.value = (float)healPoint / (float)maxHealth;
        movement();
        StateMachineMonitor();
        stateMachine.StateMachineFixedUpdate();
        if (healPoint <= 0)
        {
            
        }
    }
    public void movement()
    {
        Vector3 moveDirection = (GameManager.Instance.playerPosition.transform.position - transform.position).normalized;
        //rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        rigidbody2d.velocity = moveDirection * moveSpeed;
    }
    public void TakeDamage(int damageIncome, int criticalTier)
    {
        (int, int) damageTaken;
        damageTaken = GameCalculator.DamageTake(currentArmor, BioArmorCalculating(), damageIncome, GameManager.Instance.cellGun1.bulletPrefab.Elements);
        currentArmor.armorPoint -= damageTaken.Item2;
        healPoint -= damageTaken.Item1;
        EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, criticalTier, transform);
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     string damageSource = other.gameObject.tag;
    //     (int, int) damageTaken;
    //     (int, int) finalDamage;
    //     switch (damageSource)
    //     {
    //         case "Bullet1":
    //             finalDamage = DamageCalculator.Instance.CriticalManager(true);
    //             break;
    //         case "Bullet2":
    //             finalDamage = DamageCalculator.Instance.CriticalManager(false);
    //             damageTaken = DamageCalculator.Instance.DamageTake(currentArmor, BioArmorCalculating() , finalDamage.Item1, GameManager.Instance.cellGun2.bulletPrefab.Elements);
    //             healPoint -= damageTaken.Item1;
    //             currentArmor.armorPoint -= damageTaken.Item2;
    //             EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, finalDamage.Item2, transform);
    //             break;
    //         default:
    //             healPoint -= 0;
    //             break;
    //     }
    //     if (currentArmor.armorPoint <= baseCellArmor.armorPoint)
    //     {
    //         currentArmor.armorPoint = baseCellArmor.armorPoint;
    //     }
    //     //Debug.Log("fix armor: "+baseCellArmor.armorPoint);
    // }
    protected void StateMachineMonitor(){
        if(rigidbody2d.velocity==Vector2.zero){
            stateMachine.ChangeState(new EnemyStateIdle(this));
        }
        else if(rigidbody2d.velocity!=Vector2.zero){
            stateMachine.ChangeState(new EnemyStateMove(this));
        }
        if (healPoint <= 0)
        {
            stateMachine.ChangeState(new EnemyStateDestroy(this));
        }
        
    }
    // protected void Init(){
    //     foreach(var enemy in DataManager.Instance.Data.listEnemies){
    //         if(enemyId == enemy.enemyId){
                
    //         }
    //     }
    // }
    protected void AddProperties()
    {
        if(DataManager.Instance.Data.listEnemies.Exists(x=> x.enemyId == this.enemyId))
        {
            EnemyCellOOP enemyCellOOP = DataManager.Instance.Data.listEnemies.Find(x => x.enemyId == this.enemyId);
            maxHealth = enemyCellOOP.hp;
            healPoint = maxHealth;
            moveSpeed = enemyCellOOP.moveSpeed;
            baseCellArmor = new CellProtection(enemyCellOOP.cellProtection);
            currentArmor = new CellProtection(baseCellArmor);
            faction = enemyCellOOP.faction;
            bodyDamage = enemyCellOOP.bodyDamage;
        }
    }
    public override void OnDead()
    {
        base.OnDead();
        UpdateManager.Instance.RemoveCellFromPool(index);
        rigidbody2d.velocity = Vector3.zero;
        collider2d.enabled = false;
        destroyAnimation.SetActive(true);
        animator.SetTrigger("Destroy");
        model.color = new Color(0, 0, 0, 0);

        LeanTween.delayedCall(0.5f, () => { 
            EnemySpawner.Instance.OnEnemyDestroy(this);
            LeanPool.Despawn(gameObject); });
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
    public int bodyDamage;
}
[Serializable]
public enum Equipment{
    None,
    Melee,
    Range
}