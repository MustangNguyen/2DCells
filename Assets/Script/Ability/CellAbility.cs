using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public abstract class CellAbility : MonoBehaviour
{

    public Mutation mutation;
    public string abilityId;
    public string abilityName;
    public string mutationId;
    [Space(10)]
    [Header("Upgrade Property")]
    public int strength = 100; 
    public int duration = 100;
    public int range = 100;
    public int efficiency = 100;
    public bool isChanneling = false;
    public bool isRecastable = false;
    [Space(10)]
    [Header("Ingame Property")]
    public float inGameDuration = 0;
    public float inGameRange = 0;
    public int energyConsumption = 25;
    public float durationLeft;
    public AbilityOrder abilityOrder = AbilityOrder.Skill1;
    public CellAbility(){}
    public CellAbility(AbilityOOP abilityOOP){
        abilityId = abilityOOP.abilityId;
        abilityName = abilityOOP.abilityName;
        mutationId = abilityOOP.mutationId;
    }
    protected virtual void Start(){
        abilityId = GetType().Name;
        abilityName = DataManager.Instance.Data.listAbilities.Find(x => x.abilityId == abilityId).abilityName;
        mutationId =  DataManager.Instance.Data.listAbilities.Find(x => x.abilityId == abilityId).mutationId;
    }

    protected virtual void FixedUpdate(){
        if(durationLeft>0){
            durationLeft-=Time.fixedDeltaTime;
        }
    }

    public virtual void AbilityCast(){
        if(durationLeft<=0)
            AbilityBehavior();
        else
            if(isRecastable)
                AbilityBehavior();
    }
    abstract protected void AbilityBehavior();
    protected void SetUpBasicPropertiesOnCast(){
        mutation.currentEnery -= energyConsumption;
        durationLeft = inGameDuration;

    }
}
[Serializable]
public class AbilityOOP{
    public string abilityId;
    public string abilityName;
    public string mutationId;
    
}
public enum AbilityOrder{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Ultimate,
}