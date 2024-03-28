using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class DamageCalculator : Singleton<DamageCalculator>
{
    public int DamageTake(CellProtection cellProtection,int damageIncome, Elements elements){
        int damageTaken = 0;
        switch (cellProtection.armorType){
            case ArmorType.None:
                damageTaken = damageIncome;
                break;
            case ArmorType.Alloy:
                damageTaken = (int)((float)damageIncome * (1 - (float)cellProtection.armorPoint / (float)(cellProtection.armorPoint + ARMOR_COEFFICIENT)));
                break;
            case ArmorType.Bio:
                damageTaken = damageIncome - cellProtection.armorPoint >= damageIncome / 10 ? damageIncome - cellProtection.armorPoint : damageIncome / 10;
                break;
            default:
                damageTaken = int.MaxValue;
                break;
        }
        return damageTaken;
    }
}
