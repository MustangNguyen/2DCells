using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public partial class NetworkManager : Singleton<NetworkManager> {
    private void Start() {
        GetDataFromServer();
    }
    public void GetDataFromServer()
    {
        StartCoroutine(CreateWebGetRequest(apiUrl, (string data) =>
        {
            
        }));
    }
}