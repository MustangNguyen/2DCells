using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnCampaignBtnClick(){
        SceneLoadManager.Instance.LoadScene(SceneName.GamePlay);
    }
    public void OpenSettingPopup(){
        PopupSetting.Show();
    }
}
