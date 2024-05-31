using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class ToxinBullet : Bullet
{
    [SerializeField] private ToxinSplash toxinSplash;
    protected override void Awake()
    {
        base.Awake();
        toxinSplash = Resources.Load<ToxinSplash>("Prefab/Bullet Prefabs/ToxinArea");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyCell")
        {
            LeanPool.Spawn(toxinSplash, transform.position, transform.rotation);
            gameObject.SetActive(false);        
        }
    }
}
