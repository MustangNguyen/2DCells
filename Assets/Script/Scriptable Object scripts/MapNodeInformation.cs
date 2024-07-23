using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Node_n", menuName = "Scriptable Objects/Campaign Node")]
public class MapNodeInformation : ScriptableObject
{
    public string nodeId;
    public List<MapNodeInformation> nextNode;


}

[Serializable]
public class NodeOOP{
    public string nodeId;
    public string planetId;
    
}

[Serializable]
public class NodeProcessOOP{
    public string userId;
    public string nodeId;
    public bool isNodeFinish;
    public int nodeScore;
}
