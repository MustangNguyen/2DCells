using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CellsBase : MonoBehaviour
{
    [Header("Base Stat")]
    [SerializeField] protected int healPoint = 200;
    [SerializeField] protected int maxHealth = 200;
    [SerializeField] protected CellProtection baseCellArmor;
    [SerializeField] protected CellProtection currentArmor;
    [SerializeField] protected int defaultProtection = 0;
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected int lv = 1;

    
    
    protected virtual void OnEnable(){
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
    }
    protected virtual void Awake(){
        
    }
    protected virtual void Start(){

    }
    protected virtual void OnDead(){
        
    }
    protected int BioArmorCalculating(){
        int armor=0;
        if(baseCellArmor.armorType == ArmorType.Bio){
            armor = baseCellArmor.armorPoint + maxHealth;
        }
        return armor;
    }
}
