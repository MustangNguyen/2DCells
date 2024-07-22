using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetMapManager : MonoBehaviour
{
    [SerializeField] private GameObject nodesHolder;
    [SerializeField] private List<CampaignNode> campaignNodes;
    private void Start() {
        InitMap();
    }
    public void InitMap(){ 
        campaignNodes = new();
        campaignNodes = nodesHolder.GetComponentsInChildren<CampaignNode>().ToList();
        foreach (var node in campaignNodes)
        {
            node.SetClearNode(false);
        }
    } 
}
