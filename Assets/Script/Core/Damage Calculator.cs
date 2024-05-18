using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class DamageCalculator : Singleton<DamageCalculator>
{
    public (int, int) DamageTake(CellProtection currentCellProtection, int baseCellArmor, int damageIncome, Elements elements)
    {
        //Debug.Log("base armor:"+baseCellArmor);
        int armorReduce = 0;
        int damageTaken = 0;
        switch (currentCellProtection.armorType)
        {
            case ArmorType.None:
                damageTaken = damageIncome;
                break;
            case ArmorType.Alloy:
                damageTaken = (int)((float)damageIncome * (float)(1 - DamageReduceByArmorCalculator(currentCellProtection.armorPoint)));
                break;
            case ArmorType.Bio:
                armorReduce = damageIncome >= baseCellArmor / 20 ? damageIncome: baseCellArmor/20;
                damageTaken = damageIncome - currentCellProtection.armorPoint >= damageIncome / 20 ? damageIncome - currentCellProtection.armorPoint : damageIncome / 20;
                break;
            default:
                damageTaken = int.MaxValue;
                break;
        }
        return (damageTaken, armorReduce);
    }
    public (int, int) DamageManager(bool isCellGun1)
    {
        CellGun cellGun = isCellGun1 ? GameManager.Instance.cellGun1 :  GameManager.Instance.cellGun2;
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
