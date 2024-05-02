using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public partial class NetworkManager {
    string apiUrl = "https://localhost:7121/api/Mutations";

    public IEnumerator CreateWebGetRequest(string requestAPI,Action<string> onComplete = null, Action onFail = null){
        using(UnityWebRequest webRequest = UnityWebRequest.Get(requestAPI)){
            yield return webRequest.SendWebRequest();
            if(webRequest.result != UnityWebRequest.Result.Success){
                Debug.Log("Error while fetch from API: " + requestAPI + " " + webRequest.error);
                onFail?.Invoke();
            }
            else{
                string data = webRequest.downloadHandler.text;
                Debug.Log(data);
                onComplete?.Invoke(data);
            }
        }
    }
    public IEnumerator CreateWebPostRequest(APIRequest apiRequest,Action<string> onComplete = null,Action onFail = null)
    {
        
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiRequest.url, apiRequest.body))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(apiRequest.body);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string data = request.downloadHandler.text;
                Debug.Log("Post complete! " + data);
                onComplete?.Invoke(data);
            }
        }
    }
}

public class APIRequest{
    public string url;
    public string body;
}