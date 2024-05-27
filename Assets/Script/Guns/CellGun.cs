using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class CellGun : MonoBehaviour
{
    [SerializeField] public Bullet bulletPrefab;
    [SerializeField] protected GameObject bulletHolder;
    [SerializeField] protected bool isFirstGun = true;    
    [SerializeField] protected float fireRate = 1f;
    [Range(0f,100f)]
    [SerializeField] protected float accuracy = 70f;
    [SerializeField] public float criticalRate = 30f;
    [SerializeField] public float criticalMultiple = 2;
    [SerializeField] protected bool isAutoFire = false;
    protected bool isGunReady = true;

    protected void Start(){
        if (isFirstGun){
            InputManager.Instance.onFire += SetFire;
            InputManager.Instance.onDoubleClickLeft += SetAutoFire;
        }
        else{
            InputManager.Instance.onFire2 += SetFire;
            InputManager.Instance.onDoubleClickRight += SetAutoFire;
        }
        GameObject gameObject = GameObject.Find("Bullet Holder");
        bulletHolder = gameObject;
    }
    protected void Update()
    {
        GunRotation();
        AutoFire();
    }
    protected virtual void SetAutoFire(){
        isAutoFire = !isAutoFire;
    }
    protected virtual void AutoFire(){
        if(isAutoFire){
            //Debug.Log("on auto fire");
            SetFire();
        }
    }
    protected virtual void SetFire(){
        if(isGunReady){
            isGunReady = false;
            StartCoroutine(OnFire());
        }
        
    }
    protected virtual IEnumerator OnFire()
    {
        Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, transform.rotation, GameManager.Instance.bulletHolder);
        bullet.cellGun = this;
        if(isFirstGun)
            bullet.gameObject.tag = "Bullet1";
        else
            bullet.gameObject.tag = "Bullet2";
        bullet.SetBullet(transform, InputManager.Instance.mouseWorldPosition, accuracy);
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
