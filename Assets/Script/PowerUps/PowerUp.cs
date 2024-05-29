using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string id;
    public Mutation mutation;
    [SerializeField] protected float timeCharge = 1f;
    [SerializeField] protected float countdown = 0f;
    [SerializeField] protected int damage;
    [SerializeField] protected float scanRadius = 10f;
    [SerializeField] protected int[] layerMaskInt;
    [SerializeField] protected LayerMask layerMask;
    protected virtual void Start()
    {
        mutation = GetComponentInParent<Mutation>();
    }
    protected virtual void FixedTimeCountdown()
    {
        if (countdown <= 0)
        {
            countdown = timeCharge;
        }
        countdown -= Time.fixedDeltaTime;
    }
    protected abstract void OnFire();
}

[Serializable]
public enum PowerUpType
{
    StartUp,
    Equipment,
    Weapon,
}