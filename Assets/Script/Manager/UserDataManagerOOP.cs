using System;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManagerOOP : MonoBehaviour
{

}

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