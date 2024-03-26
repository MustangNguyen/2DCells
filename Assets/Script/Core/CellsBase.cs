using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsBase : MonoBehaviour
{
    [Header("Base Stat")]
    [SerializeField] protected int healPoint = 0;
    [SerializeField] protected CellProtection cellArmor;

    [SerializeField] protected float moveSpeed = 1f;

    protected virtual void Awake() {
        
    }
    protected virtual void Start(){

    }
}
