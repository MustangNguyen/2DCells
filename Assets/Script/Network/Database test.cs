using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Databasetest : MonoBehaviour
{
    string apiUrl = "https://localhost:44329/API/Values";
    TestClass testClass = new();
    private void Start() {
        StartCoroutine(CreateWebGetRequest());
    }
    IEnumerator CreateWebGetRequest(){
        using(UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl)){
            yield return webRequest.SendWebRequest();
            if(webRequest.result != UnityWebRequest.Result.Success){
                Debug.Log("Error while fetch from API: " + webRequest.error);
            }
            else{
                string json = webRequest.downloadHandler.text;
                Debug.Log(json);
                // var listObject = new JSONObject(json).list;
                // List<TestClass> testClassList= new();
                // foreach(var item in listObject){
                //     // testClass = JsonConvert.DeserializeObject<TestClass>(item.str);
                //     testClass.HP = (int)item["HP"].n;
                //     testClass.MP = (int)item["MP"].n;
                //     testClass.Type = item["Type"].str;
                //     testClass.ID = item["ID"].str;
                //     Debug.Log(testClass.HP + " " + testClass.MP + " " + testClass.Type + " " + testClass.ID);
                //     testClassList.Add(testClass);
                // }
            }
        }
    }
}
[Serializable]
public class TestClass{
    public int HP;
    public int MP;
    public string Type;
    public string ID;
}
