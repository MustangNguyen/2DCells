using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class EnemyGun : CellGun
{
    [HideInInspector] public float range;
    protected new void Start() {
        GameObject gameObject = GameObject.Find("Bullet Holder");
        bulletHolder = gameObject;
    }
    protected override void SetFire()
    {
        float distance = (GameManager.Instance.mutation.transform.position - transform.position).magnitude;
        if(distance<range)
            base.SetFire();
    }
    protected override IEnumerator OnFire()
    {
        Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, transform.rotation, GameManager.Instance.bulletHolder);
        bullet.gameObject.SetActive(true);
        bullet.cellGun = this;
        bullet.gameObject.tag = "EnemyBullet";

        bullet.SetBullet(transform, GameManager.Instance.mutation.transform.position, accuracy);
        yield return new WaitForSeconds(1 / fireRate);
        isGunReady = true;
    }
    public override void GunRotation()
    {
        base.GunRotation();
        Vector3 distance = GameManager.Instance.mutation.transform.position - transform.position;
        distance.Normalize();
        float rotateZ = Mathf.Atan2(distance.y,distance.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotateZ - 90);
    }
}
