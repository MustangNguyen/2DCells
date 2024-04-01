using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System.ComponentModel.Design;

public class CellGun : MonoBehaviour
{
    [SerializeField] public Bullet bulletPrefab;
    [SerializeField] protected Transform bulletHolder;
    [SerializeField] protected bool isFirstGun = true;    
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float accuracy = 70f;
    protected bool isGunReady = true;

    protected void Start(){
        if (isFirstGun){
            InputManager.Instance.onFire += SetFire;
        }
        else{
            InputManager.Instance.onFire2 += SetFire;
        }
    }
    protected void Update()
    {
        GunRotation();
    }
    protected virtual void SetFire(){
        if(isGunReady){
            isGunReady = false;
            StartCoroutine(OnFire());
        }
        
    }
    protected virtual IEnumerator OnFire()
    {
        Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, transform.rotation, bulletHolder);
        if(isFirstGun)
            bullet.gameObject.tag = "Bullet1";
        else
            bullet.gameObject.tag = "Bullet2";
        bullet.SetBullet(transform, accuracy);
        yield return new WaitForSeconds(1 / fireRate);
        isGunReady = true;
    }
    public virtual void GunRotation(){
        Vector3 distance = InputManager.Instance.mouseWorldPosition - transform.position;
        distance.Normalize();
        float rotateZ = Mathf.Atan2(distance.y,distance.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotateZ-90);
    }
}
