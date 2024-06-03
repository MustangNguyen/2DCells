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
    [SerializeField] public int bodyDamage{get; protected set;} = 0;
    [SerializeField] protected int XpObs;
    [SerializeField] public bool isRestrict = false;
    [Space(10)]
    [Header("UI")]
    [SerializeField] protected string enemyId;
    [SerializeField] public string enemyName;
    [SerializeField] public Slider healthBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected SpriteRenderer model;
    [SerializeField] protected int index;
    [SerializeField] protected Equipment equipment;
    [SerializeField] public StateMachine stateMachine;
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
        SetStatusMachine(PrimaryElement.None);
        UpdateManager.Instance.AddCellToPool(this);
        index = UpdateManager.Instance.poolIndex;
        Reset();
    }
    private void Reset()
    {
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
        model.color = new Color(1, 1, 1, 1);
        collider2d.enabled = true;
    }
    public void CellUpdate()
    {
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
            stateMachine.ChangeState(new EnemyStateDestroy(this));
        }
    }
    public void movement()
    {
        Vector2 moveDirection = (GameManager.Instance.mutation.transform.position - transform.position).normalized;
        // rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        // rigidbody2d.velocity = moveDirection * moveSpeed;
        float currentForce = moveSpeed - rigidbody2d.velocity.magnitude;
        if (rigidbody2d.velocity.magnitude > 0)
        {
            friction = rigidbody2d.velocity * -1;
            rigidbody2d.AddForce(friction * 10f * rigidbody2d.mass);
        }
        if (moveSpeed < rigidbody2d.velocity.magnitude) return;
        rigidbody2d.AddForce(moveDirection * currentForce * 20 * rigidbody2d.mass);
    }
    public void TakeDamage(int damageIncome, int criticalTier, string status = null)
    {
        (int, int) damageTaken;
        damageTaken = GameCalculator.DamageTake(currentArmor, BioArmorCalculating(), damageIncome);
        currentArmor.armorPoint -= damageTaken.Item2;
        healPoint -= damageTaken.Item1;
        EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, criticalTier, transform, status);
    }
    public void SetStatusMachine(PrimaryElement element, int damageIncome = 0, int stack = 0,bool isOverrideMaxStack = false)
    {
        switch (element)
        {
            case PrimaryElement.Fire:
                stateMachine.ChangeStatusState(new StatusStateBurn(this, PrimaryElement.Fire, damageIncome, stack));
                break;
            case PrimaryElement.Ice:
                stateMachine.ChangeStatusState(new StatusStateFreeze(this,PrimaryElement.Ice,stack,isOverrideMaxStack));
                break;
            case PrimaryElement.Toxin:
                stateMachine.ChangeStatusState(new StatusStatePoisoned(this,PrimaryElement.Toxin, damageIncome,stack));
                break;
            case PrimaryElement.Electric:
                stateMachine.ChangeStatusState(new StatusStateShock(this,PrimaryElement.Electric, damageIncome,stack,isOverrideMaxStack));
                break;
            default:
                stateMachine.ChangeStatusState(new StatusStateNormal(this));
                break;
        }
    }
    protected void StateMachineMonitor()
    {
        if(isRestrict) return;
        if (rigidbody2d.velocity == Vector2.zero)
        {
            stateMachine.ChangeState(new EnemyStateIdle(this));
        }
        else if (rigidbody2d.velocity != Vector2.zero)
        {
            stateMachine.ChangeState(new EnemyStateMove(this));
        }

    }
    // protected void Init(){
    //     foreach(var enemy in DataManager.Instance.Data.listEnemies){
    //         if(enemyId == enemy.enemyId){

    //         }
    //     }
    // }
    protected void  AddProperties()
    {
        if (DataManager.Instance.Data.listEnemies.Exists(x => x.enemyId == this.enemyId))
        {
            EnemyCellOOP enemyCellOOP = DataManager.Instance.Data.listEnemies.Find(x => x.enemyId == this.enemyId);
            maxHealth = enemyCellOOP.hp;
            enemyName = enemyCellOOP.enemyName;
            healPoint = maxHealth;
            moveSpeed = enemyCellOOP.moveSpeed;
            defaultMoveSpeed = enemyCellOOP.moveSpeed;
            baseCellArmor = new CellProtection(enemyCellOOP.cellProtection);
            currentArmor = new CellProtection(baseCellArmor);
            faction = enemyCellOOP.faction;
            bodyDamage = enemyCellOOP.bodyDamage;
            XpObs = enemyCellOOP.XpObs;
        }
    }
    public override void OnDead()
    {
        base.OnDead();
        moveSpeed = 0;
        UpdateManager.Instance.RemoveCellFromPool(index);
        rigidbody2d.velocity = Vector3.zero;
        collider2d.enabled = false;
        destroyAnimation.SetActive(true);
        animator.SetTrigger("Destroy");
        model.color = new Color(0, 0, 0, 0);
        EffectManager.Instance.SpawnObs(gameObject,XpObs);
        LeanTween.delayedCall(1f, () =>
        {
            EnemySpawner.Instance.OnEnemyDestroy(this);
            LeanPool.Despawn(gameObject);
        });
    }
}
[Serializable]
public class EnemyCellOOP
{
    public string enemyId;
    public string enemyName;
    public int hp;
    public int mp;
    public CellProtection cellProtection;
    public float moveSpeed;
    public string abilityId;
    public Faction faction;
    public Equipment equipment;
    public int XpObs;
    public EnemyCellOOP()
    {
        cellProtection = new CellProtection();
    }
    public int bodyDamage;
}
[Serializable]
public enum Equipment
{
    None,
    Melee,
    Range
}