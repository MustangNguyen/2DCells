using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSimple : Bullet
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void SetBullet(Transform gunPosition, float accuracy)
    {
        base.SetBullet(gunPosition, accuracy);
        bulletCollider2D.enabled = true;
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        bulletCollider2D.enabled = false;
    }
}
