using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignNode : MonoBehaviour
{
    public MapNodeInformation nodeInformation;
    public Image nodeClear;
    public Image nodeNotClear;
    public Image nodeNotReach;
    public bool isCleared = false;
    private void Start() {
        SetClearNode(DataManager.Instance.UserData.userNodeProcesses.Exists(x => x.nodeId == nodeInformation.nodeId && x.isNodeFinish == true)
        ,DataManager.Instance.UserData.userNodeProcesses.Exists(x => x.nodeId == nodeInformation.nodeId && x.isNodeFinish == false));
    }
    private void OnEnable() {
    }
    public void SetClearNode(bool isClear = false,bool isReach = false){
        if (isClear){
            nodeClear.gameObject.SetActive(true);
            nodeNotClear.gameObject.SetActive(false);
            nodeNotReach.gameObject.SetActive(false);
        }
        else{
            if(isReach){
                nodeClear.gameObject.SetActive(false);
                nodeNotClear.gameObject.SetActive(true);
                nodeNotReach.gameObject.SetActive(false);
            }
            else{
                nodeClear.gameObject.SetActive(false);
                nodeNotClear.gameObject.SetActive(false);
                nodeNotReach.gameObject.SetActive(true);
            }
        }
    }
    public void OnNodeClick(){
        SceneLoadManager.Instance.mapNodeInformation = nodeInformation;
        SceneLoadManager.Instance.LoadScene(SceneName.GamePlay);
    }
}
