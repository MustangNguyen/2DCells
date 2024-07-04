using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataManagerOOP
{
    public UserInformation userInformation = new();
    public List<UserGunInformation> userGunInformation = new();
    public List<UserEquipedGunInfor> userEquipedGunInfors = new();
    public List<MutationOOP> listMutations = new();
    public List<EnemyCellOOP> listEnemies = new();
    public List<AbilityOOP> listAbilities = new();
    public List<BulletOOP> listBullet = new();
    public List<IngameLevelConfigsOOP> listIngameLevelConfig = new();
    public List<CellGunOOP> listGun = new();
}

[Serializable]
public class UserInformation{
    public string userID;
    public string userName;
    public string email;
}

[Serializable]
public class UserGunInformation {
    public string ownerShipId;
    public string userId;
    public string gunId;
    public int gunLv;
    public int gunXp;
}
[Serializable]
public class UserEquipedGunInfor
{
    public string userEquipmentId;
    public string userId;
    public string mutationOwnershipId;
    public string gunOwnershipId1;
    public string gunOwnershipId2;
}

