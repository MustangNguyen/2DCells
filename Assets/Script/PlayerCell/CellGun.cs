using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CellGun : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected Transform bulletHolder;
    [SerializeField] protected bool isFirstGun = true;    
    [SerializeField] protected float fireRate = 1f;
    protected bool isGunReady = true;

    protected void Start(){
        if (isFirstGun)
            InputManager.Instance.onFire += SetFire;
        else
            InputManager.Instance.onFire2 += SetFire;
    }
    void Update()
    {
        GunRotation();
    }
    protected void SetFire(){
        StartCoroutine(Reload());
        
    }
    protected IEnumerator Reload(){
        if(isGunReady){
            Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, transform.rotation, bulletHolder);
            bullet.SetBullet(transform);
            isGunReady = false;
        }
        yield return new WaitForSeconds(fireRate);
        isGunReady = true;
    }
    public void GunRotation(){
        Vector3 distance = InputManager.Instance.mouseWorldPosition - transform.position;
        distance.Normalize();
        float rotateZ = Mathf.Atan2(distance.y,distance.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotateZ-90);
    }
}
