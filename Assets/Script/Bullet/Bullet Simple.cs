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
        
        
    }
    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
    }
}
