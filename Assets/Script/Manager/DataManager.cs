using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private DataManagerOOP data;

    public DataManagerOOP Data
    {
        get { return data; }
        set { Data = data; }
    }
    private void GetMutationData(string data){
        JSONObject json = new JSONObject(data);
    }
}
