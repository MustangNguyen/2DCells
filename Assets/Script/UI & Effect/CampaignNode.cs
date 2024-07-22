using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignNode : MonoBehaviour
{
    public MapNodeInformation nodeInformation;
    public Image nodeClear;
    public Image nodeNotClear;
    public bool isCleared = false;
    private void Start() {
        nodeClear.gameObject.SetActive(false);
        nodeNotClear.gameObject.SetActive(true);
    }
    public void SetClearNode(bool isClear = false){
        if (isClear){
            nodeClear.gameObject.SetActive(true);
            nodeNotClear.gameObject.SetActive(false);
        }
        else{
            nodeClear.gameObject.SetActive(false);
            nodeNotClear.gameObject.SetActive(true);
        }
    }
    public void OnNodeClick(){
        SceneLoadManager.Instance.LoadScene(SceneName.GamePlay);
    }
}
