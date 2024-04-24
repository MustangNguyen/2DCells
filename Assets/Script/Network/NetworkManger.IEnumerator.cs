using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public partial class NetworkManager {
    string apiUrl = "https://localhost:44329/api/mutations";

    public IEnumerator CreateWebGetRequest(string requestAPI,Action<string> onComplete = null, Action onFail = null){
        using(UnityWebRequest webRequest = UnityWebRequest.Get(requestAPI)){
            yield return webRequest.SendWebRequest();
            if(webRequest.result != UnityWebRequest.Result.Success){
                Debug.Log("Error while fetch from API: " + webRequest.error);
                onFail?.Invoke();
            }
            else{
                string data = webRequest.downloadHandler.text;
                Debug.Log(data);

                onComplete?.Invoke(data);
            }
        }
    }
}