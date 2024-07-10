using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



[Serializable]
public class UserDataOOP
{
    public string userId;
    public string userName;
    public string password;
    public int credit;
    public UserDataOOP(){}
    public UserDataOOP(string userId, string userName, string password, int credit)
    {
        this.userId = userId;
        this.userName = userName;
        this.password = password;
        this.credit = credit;
    }
}
[Serializable]
public class UserLogin{
    public string email;
    public string password;
    public UserLogin(){}
    public UserLogin(string email,string password){
        this.email = email;
        this.password = password;
    }
}
/*
[Serializable]
public class UserGun
{
    public string ownershipId;
    public string userId;
    public string gunId;
    public int gunLv;
    public int gunXp;
    public UserGun() { }

    public UserGun(string ownerShipId, string userId, string gunId, int gunLv, int gunXp)
    {
        this.ownershipId = ownerShipId;
        this.userId = userId;
        this.gunId = gunId;
        this.gunLv = gunLv;
        this.gunXp = gunXp;
    }
}*/

[Serializable]
public class AccessToken{
    public string accessToken;
    public string refreshToken;
}
