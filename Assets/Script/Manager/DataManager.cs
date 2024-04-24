using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DataManagerOOP Data = new();

    private void Start() {

    }

    public void GetMutationData(string data){
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach(var item in listData){
            MutationOOP mutation = new MutationOOP();
            mutation.maxHealth = (int)item["HP"].n;
            mutation.maxEnery = (int)item["MP"].n;
            ArmorType armorType;
            Enum.TryParse(item["CellProtection"].str,out armorType);
            ShieldType shieldType;
            Enum.TryParse(item["CellProtection"].str,out shieldType);
            mutation.baseCellProtection.armorType = armorType;
            mutation.baseCellProtection.armorPoint = (int)item["Armor"].n;
            mutation.baseCellProtection.shieldType = shieldType;
            mutation.baseCellProtection.shieldPoint = (int)item["Shield"].n;
            mutation.moveSpeed = (float)item["MoveSpeed"].n;
            mutation.mutationID = item["MutationID"].str;
            mutation.mutationName = item["MutationName"].str;
            foreach(var ability in item["mutation_abilities"].list){
                Ability tempAbility = new Ability();
                tempAbility.abilityID = ability["AbilityID"].str;
                tempAbility.abilityName = ability["AbilityName"].str;
                tempAbility.mutationID = ability["MutationID"].str;
                mutation.mutationAbilities.Add(tempAbility);
            }
            Data.listMutations.Add(mutation);
        }
    }
}
