using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using TMPro;

public class EnemyCell : CellsBase
{
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [Space(10)]
    [Header("UI")]
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected int index;
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateManager.Instance.AddCellToPool(this);
        index = UpdateManager.Instance.poolIndex;
    }
    public void CellUpdate(){
        healthBar.value = (float)healPoint / (float)maxHealth;
        healthText.text = healPoint.ToString();
        movement();
        if(healPoint<=0){
            OnDead();
        }

    }
    public void movement(){
        Vector3 moveDirection = GameManager.Instance.playerPosition.transform.position - transform.position;
        Vector3.Normalize(moveDirection);
        rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
    }
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        string damageSource = other.gameObject.tag;
        switch (damageSource)
        {
            case "Bullet1":
                healPoint -= GameManager.Instance.cellGun1.bulletPrefab.Damage;
                break;
            case "Bullet2":
                healPoint -= GameManager.Instance.cellGun1.bulletPrefab.Damage;
                break;
            default:
                healPoint -= 0;
                break;
        }
    }
    
    protected override void OnDead()
    {
        base.OnDead();
        UpdateManager.Instance.RemoveCellFromPool(index);
        LeanPool.Despawn(gameObject);
    }
}
