using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static GameStatic;
using UnityEngine.Video;

public partial class NetworkManager : Singleton<NetworkManager>
{
    private AccessToken accessToken = new AccessToken();
    APIRequest apiRequest = new();
    void Start()
    {
        // UserDataOOP user = new UserDataOOP();
        // user.userId = "userID1234";
        // user.userName = "exampleUser";
        // user.password = "examplePassword";
        // user.credit = 100;

        // PostNewUserToServer(user);
    }
    #region Get
    public void GetIngameLevelConfigsFromServer(Action onFinish = null){
        StartCoroutine(CreateWebGetRequest(HOST + GET_INGAME_LEVEL_CONFIGS,(string data)=>{
            DataManager.Instance.GetIngameLevelConfigs(data);
            onFinish?.Invoke();
        }));
    }
    public void GetMutationDataFromServer(Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_MUTATION_API, (string data) =>
        {
            DataManager.Instance.GetMutationData(data);
            onFinish?.Invoke();
        }));
    }
    public void GetEnemyDataFromServer(Action onFinish = null){
        StartCoroutine(CreateWebGetRequest(HOST + GET_ENEMY_API,(string data) => 
        {
            DataManager.Instance.GetEnemydata(data);
            onFinish?.Invoke();
        }));
    }
    public void GetAbilityDataFromServer(Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_ABILITY_API, (string data) =>
        {
            DataManager.Instance.GetAbilityData(data);
            onFinish?.Invoke();
        }));
    }
    public void GetBulletDataFromServer(Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_BULLET_API, (string data) =>
        {
            DataManager.Instance.GetBulletData(data);
            onFinish?.Invoke();
        }));
    }
    public void GetUserInformationFromServer(string email,Action onFinish = null){
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_INFORMATION + email, (string data) =>
        {
            DataManager.Instance.GetUserInformationData(data);
            onFinish?.Invoke();
        }));
    }
    public void GetUserGunFromServer(string userId,Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_GUN + userId, (string data) => 
        {
            DataManager.Instance.GetUserGunInformationData(data);
            onFinish?.Invoke();
        }));
    }
    public void GetGunFromServer(Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_GUN_API, (string data) =>
        {
            DataManager.Instance.GetGunData(data);   
            onFinish?.Invoke();
        }));
    }
    public void GetUserEquipedGunFromServer(string userId,Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_EQUIPED_GUN + userId , (string data) =>
        {
            DataManager.Instance.GetUserEquipedGunInfor(data);
            onFinish?.Invoke();
        }));
    }
    public void GetUserMutattionFromServer(string userId,Action onFinish = null)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_MUTATION + userId, (string data) =>
        {
            DataManager.Instance.GetUserMutationInfor(data);
            onFinish?.Invoke();
        }));
    }
    public void GetPlanetsFromServer(Action onFinish = null){
        StartCoroutine(CreateWebGetRequest(HOST + GET_PLANETS_API, (string data) =>
        {
            DataManager.Instance.GetPlanetsData(data);
            onFinish?.Invoke();
        }));
    }
    #endregion

    #region Post
    public void PostRequestSignUp(UserLogin userLogin,Action onComplete, Action<string> onFail){
        APIRequest aPIRequest = new();
        apiRequest.url = HOST + POST_SIGNUP_REQUEST;
        string jsonData = JsonConvert.SerializeObject(userLogin);
        apiRequest.body = jsonData;
        StartCoroutine(CreateWebPostRequest(apiRequest,(string data)=>{
            onComplete?.Invoke();
        },(data)=>{
            onFail?.Invoke(data);
        },false));
    }
    public void PostRequestLogin(UserLogin userLogin,Action onComplete,Action onFail,Action onUnauthorized){
        APIRequest aPIRequest = new();
        apiRequest.url = HOST + POST_LOGIN_REQUEST;
        string jsonData = JsonConvert.SerializeObject(userLogin);
        apiRequest.body = jsonData;
        StartCoroutine(CreateWebPostRequest(apiRequest,(string data)=>{
            JSONObject json = new JSONObject(data);
            accessToken.accessToken = json["accessToken"].str;
            accessToken.refreshToken = json["refreshToken"].str;
            onComplete?.Invoke();
        },(data)=>{
            onFail?.Invoke();
        },false,onUnauthorized:()=>{
            onUnauthorized?.Invoke();
        }));
    }
    //public void PostNewUserToServer(UserDataOOP newUser)
    //{
    //    APIRequest apiRequest = new();
    //    apiRequest.url = HOST + "/api/Users";
    //    string jsonData = JsonConvert.SerializeObject(newUser);
    //    apiRequest.body = jsonData;
    //    StartCoroutine(CreateWebPostRequest(apiRequest, (string data) =>
    //    {
    //        Debug.Log("done");
    //    }));
    //}
    public static APIRequest UpdateEquipmentSet(string equipSetId, string mutationId, string gun1 = null , string gun2  =null)
    {
        APIRequest apiRequest = new();
        apiRequest.url = HOST + "/api/UserEquipment/UpdateUserEquipment";
        var currentSet = DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipSetId);
        var data = new
        {
            userEquipmentId = currentSet.userEquipmentId,
            mutationOwnershipId = currentSet.mutationOwnershipId,
            gunOwnershipId1 = gun1,
            gunOwnershipId2 = gun2,
        };
        apiRequest.body = JsonConvert.SerializeObject(data);
        return apiRequest;
    }
    public void PostUpdateUserEquipment(){

    }
    public void PostUpdateUserProcess(NodeProcessOOP nodeProcessOOP,Action<string> onComplete,Action<string> onFail){
        APIRequest aPIRequest = new();
        apiRequest.url = HOST + POST_UPDATE_USER_PROCESS;
        string jsonData = JsonConvert.SerializeObject(nodeProcessOOP);
        apiRequest.body = jsonData;
        StartCoroutine(CreateWebPostRequest(apiRequest,(string data)=>{
            JSONObject json = new JSONObject(data);
            onComplete?.Invoke(data);
        },(data)=>{
            onFail?.Invoke(data);
        },true));
    }
    #endregion
}