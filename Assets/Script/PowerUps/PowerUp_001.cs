using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PowerUp_001 : PowerUp
{
    [SerializeField] private float explosiveRadius;
    [SerializeField] private float pushForce;
    [SerializeField] private Bullet bullet;
    protected override void Start() {
        base.Start();
        bullet = Resources.Load<Bullet>("Prefab/Bullet Prefabs/FireBall");
    }
    private void FixedUpdate() {
        FixedTimeCountdown();
        if(countdown <=0){
            OnFire();
        }
    }
    protected override void OnFire()
    {
        Collider2D []enemyArray = Physics2D.OverlapCircleAll(mutation.transform.position,scanRadius,layerMask);
        if (enemyArray.Length > 0)
        {
            var nearestEnemy = enemyArray[0];
            for (int i = 1; i < enemyArray.Length; i++)
            {
                if (Vector2.Distance(enemyArray[i].transform.position, mutation.transform.position) < Vector2.Distance(nearestEnemy.transform.position, mutation.transform.position))
                {
                    nearestEnemy = enemyArray[i];
                }
            }
            Bullet spawnedBullet = LeanPool.Spawn(bullet, transform.position, Quaternion.identity, GameManager.Instance.bulletHolder);
            Vector3 distance = nearestEnemy.transform.position - transform.position;
            distance.Normalize();
            float rotateZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 90);
            spawnedBullet.SetBullet(transform, nearestEnemy.transform.position, 100f);
        }

    }
    private void OnCollisionEnter2D(Collision2D other) {
        
    }
}
