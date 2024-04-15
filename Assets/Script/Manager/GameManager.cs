using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPosition;
    public int maximumEnemies = 50;
    public CellGun cellGun1;
    public CellGun cellGun2;
    public Camera currentCamera;
    private void Start()
    {

    }
    public (int, int) DamageManager(bool isCellGun1)
    {
        CellGun cellGun = isCellGun1 ? cellGun1 : cellGun2;
        int criticalCoeffident = Random.Range(0, 101);
        if (cellGun.criticalRate < 100)
        {
            return criticalCoeffident >= cellGun.criticalRate ?
            (cellGun.bulletPrefab.Damage, 0)
            : ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (100 <= cellGun.criticalRate && cellGun.criticalRate < 200)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100)), (int)cellGun.criticalRate / 100)
            : ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (200 <= cellGun.criticalRate && cellGun.criticalRate < 300)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100)), (int)cellGun.criticalRate / 100)
            : ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (300 <= cellGun.criticalRate && cellGun.criticalRate < 400)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100)), (int)cellGun.criticalRate / 100)
            : ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (400 <= cellGun.criticalRate && cellGun.criticalRate < 500)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100)), (int)cellGun.criticalRate / 100)
            : ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        }
        else
        {
            return ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100)), (int)cellGun.criticalRate / 100);
        }
        // if (criticalCoeffident < cellGun.criticalRate % 100)
        // {
        //     return ((int)(cellGun.bulletPrefab.Damage * cellGun.criticalMultiple * ((int)cellGun.criticalRate / 100 + 1)), (int)cellGun.criticalRate / 100 + 1);
        // }
        // return (cellGun.bulletPrefab.Damage, 0);
    }
}
