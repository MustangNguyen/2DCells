using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Mutation : CellsBase
{
    [SerializeField] protected Rigidbody2D playerRigidbody2d;
    [SerializeField] protected float pushBackForce = 1f;
    [SerializeField] protected string mutationId;
    [SerializeField] protected string mutationName;
    [SerializeField] protected StateMachine stateMachine;
    protected bool isMoving = true;
    protected float shipAngle = 0f;
    protected bool isDelaying = false;
    protected float delayTime = 0;
    public float rotationInterpolation = 0.4f;
    public List<Ability> mutationAbilities;
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Update(){
        StateMachineMonitor();
        AbilityTrigger();
    }
    protected virtual void FixedUpdate() {
        isMoving = true;
        if(InputManager.Instance.GetArrowButton() == Vector3.zero){
            isMoving = false;
        }
        PlayerMovement();
        PlayerRotation();
        ShieldRecharge();
        ShieldDelay();
    }
    protected override void Start(){
        base.Start();
        stateMachine = GetComponent<StateMachine>();
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        pushBackForce = 1000;
        AddProperties();
        ShowProterties();
        stateMachine.ChangeState(new PlayerStateIdle(this));
        shieldRechargeRate = GameCalculator.ShieldRechargeCalculator(baseCellArmor.shieldPoint);
    }
    protected void AbilityTrigger(){
        if(InputManager.Instance.Ability1Button)
            Ability1();
        if(InputManager.Instance.Ability2Button)
            Ability2();
        if(InputManager.Instance.Ability3Button)
            Ability3();
    }
    protected virtual void Ability1(){
        Debug.Log("Ability 1 triggered!");
    }
    protected virtual void Ability2(){
        Debug.Log("Ability 2 triggered!");

    }
    protected virtual void Ability3(){
        Debug.Log("Ability 3 triggered!");

    }
    public void PlayerRotation(){
        Vector2 lookDir = InputManager.Instance.GetArrowButton();
        lookDir.x *= -1;
        if (isMoving)
        {
            shipAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        }

        if (playerRigidbody2d.rotation <= -90 && shipAngle >= 90){
            playerRigidbody2d.rotation += 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }

        if (playerRigidbody2d.rotation >= 90 && shipAngle <= -90){
            playerRigidbody2d.rotation -= 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }else
        {
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }
    }
    protected void PlayerMovement(){
        Vector2 moveDirection = InputManager.Instance.GetArrowButton();
        //playerRigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        playerRigidbody2d.velocity = moveDirection * moveSpeed ;
    }
    
    protected void ShieldRecharge(){
        // Debug.Log(isDelaying);
        // Debug.Log(currentArmor.shieldPoint + " + " + baseCellArmor.shieldPoint);
        if (currentArmor.shieldPoint < baseCellArmor.shieldPoint && !isDelaying)
        {
            currentArmor.shieldPoint += (int)(shieldRechargeRate * Time.fixedDeltaTime);
            if (currentArmor.shieldPoint > baseCellArmor.shieldPoint)
                currentArmor.shieldPoint = baseCellArmor.shieldPoint;
            GameManager.Instance.healthBar.AdjustShield((float)currentArmor.shieldPoint / baseCellArmor.shieldPoint, currentArmor.shieldPoint.ToString());
        }
    }
    protected void StateMachineMonitor(){
        if(playerRigidbody2d.velocity==Vector2.zero){
            stateMachine.ChangeState(new PlayerStateIdle(this));
        }
        else if(playerRigidbody2d.velocity!=Vector2.zero){
            stateMachine.ChangeState(new PlayerStateMove(this));
        }
    }

    /* +++ ON CONSTRUCTION +++ */
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyCell")
        {
            if(currentArmor.shieldPoint>0){

                currentArmor.shieldPoint -=50;
                if(currentArmor.shieldPoint<0)
                    currentArmor.shieldPoint = 0;
                delayTime = GameCalculator.ShieldRechargeDelayCalculator(baseCellArmor.shieldPoint - currentArmor.shieldPoint);
                GameManager.Instance.healthBar.AdjustShield((float)currentArmor.shieldPoint/baseCellArmor.shieldPoint,currentArmor.shieldPoint.ToString());
            }
            else{

                healPoint -= 10;
                GameManager.Instance.healthBar.AdjustHealth((float)healPoint/maxHealth,healPoint.ToString());
            }
            EffectManager.Instance.ShowDamageInfict(10,4,transform);

            Vector2 collisionDirection = other.contacts[0].normal.normalized;
            //Debug.Log(collisionDirection * pushBackForce);
            playerRigidbody2d.AddForce(collisionDirection * pushBackForce, ForceMode2D.Force);
        }
    }
    protected void ShieldDelay()
    {
        if (delayTime > 0)
        {
            isDelaying = true;
            delayTime -= Time.fixedDeltaTime;
        }
        else
        {
            isDelaying = false;
        }
    }
    protected void GetRotation()
    {
        
    }
    protected void AddProperties(){
        if(DataManager.Instance.Data.listMutations.Exists(x => x.mutationID == mutationId)){
            MutationOOP mutationOOP = DataManager.Instance.Data.listMutations.Find(x => x.mutationID == mutationId);
            mutationName = mutationOOP.mutationName;
            maxHealth = mutationOOP.maxHealth;
            maxEnery = mutationOOP.maxEnery;
            baseCellArmor = mutationOOP.baseCellProtection;
            currentArmor = new CellProtection(baseCellArmor);
            moveSpeed = mutationOOP.moveSpeed;
            lv = mutationOOP.lv;
            currentArmor.shieldPoint = baseCellArmor.shieldPoint;
            healPoint = maxHealth;
            currentEnery = maxEnery;
        }
    }
    protected void ShowProterties(){
        GameManager.Instance.healthBar.shieldText.text = baseCellArmor.shieldPoint.ToString();
        GameManager.Instance.healthBar.healthText.text = maxHealth.ToString();
        GameManager.Instance.healthBar.energyText.text = maxEnery.ToString();
    }
}

[Serializable]
public class MutationOOP
{
    [Header("Base Stat")]
    public string mutationID;
    public string mutationName;
    public int maxHealth = 200;
    public int maxEnery = 200;
    public CellProtection baseCellProtection;
    public float moveSpeed = 1f;
    public int lv = 1;
    public List<Ability> mutationAbilities;
    public Faction faction;
    public MutationOOP()
    {
        baseCellProtection = new CellProtection();
        mutationAbilities = new List<Ability>();
    }
}