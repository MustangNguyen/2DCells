using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] protected int damage = 20;
    [SerializeField] protected bool isProjectile = true;
    [SerializeField] protected Rigidbody2D rigidbody2d;


    public void BulletMovement()
    {

    }
    private void Start() {
    }

    public void SetBullet(Transform gunPosition)
    {
        Vector2 bulletDirection = InputManager.Instance.mouseWorldPosition - gunPosition.position;
        bulletDirection.Normalize();
        rigidbody2d.AddForce(bulletDirection * bulletSpeed,ForceMode2D.Impulse);
    }
}
