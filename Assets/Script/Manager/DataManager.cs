using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DataManagerOOP Data = new();

    private void Start() {
        NetworkManager.Instance.GetMutationDataFromServer();
        NetworkManager.Instance.GetEnemyDataFromSever();
    }

    public void GetMutationData(string data){
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach(var item in listData){
            MutationOOP mutation = new MutationOOP();
            mutation.maxHealth = (int)item["hp"].n;
            mutation.maxEnery = (int)item["mp"].n;
            ArmorType armorType;
            Enum.TryParse(item["cellProtection"].str,out armorType);
            ShieldType shieldType;
            Enum.TryParse(item["cellProtection"].str,out shieldType);
            mutation.baseCellProtection.armorType = armorType;
            mutation.baseCellProtection.armorPoint = (int)item["armor"].n;
            mutation.baseCellProtection.shieldType = shieldType;
            mutation.baseCellProtection.shieldPoint = (int)item["shield"].n;
            mutation.moveSpeed = (float)item["moveSpeed"].n;
            mutation.mutationID = item["mutationId"].str;
            mutation.mutationName = item["mutationName"].str;
            Faction faction;
            Enum.TryParse(item["factionId"].str,out faction);
            mutation.faction = faction;
            foreach(var ability in item["mutationAbilities"].list){
                Ability tempAbility = new Ability();
                tempAbility.abilityID = ability["abilityId"].str;
                tempAbility.abilityName = ability["abilityName"].str;
                tempAbility.mutationID = ability["mutationId"].str;
                mutation.mutationAbilities.Add(tempAbility);
            }
            Data.listMutations.Add(mutation);
        }
    }
    public void GetEnemydata(string data){
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach(var item in listData){
            EnemyCellOOP enemyCell = new();
            enemyCell.enemyId = item["enemyId"].str;
            enemyCell.enemyName = item["enemyName"].str;
            enemyCell.hp = (int)item["hp"].n;
            enemyCell.mp = (int)item["mp"].n;
            ArmorType armorType;
            Enum.TryParse(item["cellProtection"].str,out armorType);
            ShieldType shieldType;
            Enum.TryParse(item["cellProtection"].str,out shieldType);
            enemyCell.cellProtection.armorType = armorType;
            enemyCell.cellProtection.armorPoint = (int)item["armor"].n;
            enemyCell.cellProtection.shieldType = shieldType;
            enemyCell.cellProtection.shieldPoint = (int)item["armor"].n;
            Faction faction;
            Enum.TryParse(item["factionId"].str,out faction);
            enemyCell.faction = faction;
            enemyCell.abilityId = item["abilityId"].str;
            enemyCell.moveSpeed = (float)item["moveSpeed"].n;
            Equipment equipment;
            Enum.TryParse(item["equipment"].str, out equipment); 
            enemyCell.equipment = equipment;
            Data.listEnemies.Add(enemyCell);
        }   
    }
}
