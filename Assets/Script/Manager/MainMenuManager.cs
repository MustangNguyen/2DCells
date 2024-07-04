using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.StartMainMenuBackGround();
    }
    public void OnCampaignBtnClick(){
        SceneLoadManager.Instance.LoadScene(SceneName.GamePlay);
    }
    public void OpenSettingPopup(){
        PopupSetting.Show();
    }
    public void OnCollectionBtnClick()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.Collection);

    }
    public void OnEquipmentBtnClick()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.Equipment);
    }
    public void OnQuitBtnClick(){
        Application.Quit();
    }
}
