using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] protected int damage = 20;
    [SerializeField] protected Elements elements;
    [SerializeField] protected float timeExist = 2f;
    [SerializeField] protected bool isProjectile = true;
    [SerializeField] protected Rigidbody2D rigidbody2d;

    public int Damage {get => damage;}
    public Elements Elements {get => elements;}
    protected virtual void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        if(rigidbody2d == null){
            rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody2d.gravityScale = 0;
    }
    public void BulletMovement()
    {

    }
    private void Start() {

    }

    public virtual void SetBullet(Transform gunPosition)
    {
        Vector2 bulletDirection = InputManager.Instance.mouseWorldPosition - gunPosition.position;
        bulletDirection.Normalize();
        rigidbody2d.AddForce(bulletDirection * bulletSpeed,ForceMode2D.Impulse);
        LeanTween.delayedCall(timeExist, () =>
        {
            LeanPool.Despawn(gameObject);
        });
    }
}
