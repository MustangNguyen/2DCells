using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataManagerOOP
{
    public UserInformation userInformation = new();
    public List<MutationOOP> listMutations = new();
    public List<EnemyCellOOP> listEnemies = new();
    public List<AbilityOOP> listAbilities = new();
    public List<BulletOOP> listBullet = new();
    public List<IngameLevelConfigsOOP> listIngameLevelConfig = new();
}

[Serializable]
public class UserInformation{
    public string userID;
    public string userName;
    public string email;
}
