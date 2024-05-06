using System;
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
    [SerializeField] protected int maxEnery = 200;
    [SerializeField] protected int currentEnery = 200;
    [SerializeField] protected CellProtection baseCellArmor;
    [SerializeField] protected CellProtection currentArmor;
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected int lv = 1;
    [SerializeField] protected Faction faction;
    protected float shieldRechargeDelay = 1;
    protected float shieldRechargeRate = 100;

    
    
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
[Serializable]
public enum Faction{
    Hematos,
    Neutroton,
    Cytocell,
    Carcino
}