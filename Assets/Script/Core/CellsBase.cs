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
    [SerializeField] protected CellProtection cellArmor;
    [SerializeField] protected int protectionPoint = 0;
    [SerializeField] protected int defaultProtection = 0;
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected int lv = 1;

    
    
    protected virtual void OnEnable(){
        healPoint = maxHealth;
        protectionPoint = cellArmor.armorPoint;
    }
    protected virtual void Awake(){
        
    }
    protected virtual void Start(){

    }
    protected virtual void OnDead(){
        
    }
}
