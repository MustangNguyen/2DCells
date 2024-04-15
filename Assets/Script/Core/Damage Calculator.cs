using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class DamageCalculator : Singleton<DamageCalculator>
{
    public (int, int) DamageTake(CellProtection currentCellProtection, int baseCellArmor, int damageIncome, Elements elements)
    {
        Debug.Log("base armor:"+baseCellArmor);
        int armorReduce = 0;
        int damageTaken = 0;
        switch (currentCellProtection.armorType)
        {
            case ArmorType.None:
                damageTaken = damageIncome;
                break;
            case ArmorType.Alloy:
                damageTaken = (int)((float)damageIncome * (1 - (float)currentCellProtection.armorPoint / (float)(currentCellProtection.armorPoint + ARMOR_COEFFICIENT)));
                break;
            case ArmorType.Bio:
                armorReduce = damageIncome >= baseCellArmor / 20 ? damageIncome: baseCellArmor/20;
                damageTaken = damageIncome - currentCellProtection.armorPoint >= damageIncome / 20 ? damageIncome - currentCellProtection.armorPoint : damageIncome / 20;
                break;
            default:
                damageTaken = int.MaxValue;
                break;
        }
        Debug.Log("armor reduce:"+armorReduce);
        return (damageTaken, armorReduce);
    }
}
