using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CellAbility : MonoBehaviour
{
    public string abilityId;
    public string abilityName;
    public int strength;
    public int duration;
    public int range;
    public int efficiency;
    protected void Start(){
        abilityId = DataManager.Instance.Data.listAbilities.Find(x => x.abilityId == abilityId).abilityId;
        abilityName = DataManager.Instance.Data.listAbilities.Find(x => x.abilityName == abilityName).abilityName;
    }
}
[Serializable]
public class AbilityOOP{
    public string abilityId;
    public string abilityName;
    
}
public enum AbilityType{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Ultimate,
}