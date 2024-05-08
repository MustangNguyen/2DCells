using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupSetting : Popups
{
    public static PopupSetting Instance;

    private Action<bool> _onResult;
    void InitUI()
    {
        // Frame.color = UserUIManager.Instance.GetCurrentUIColor();
    }

    public void OnYesButtonClicked()
    {
        _onResult?.Invoke(true);
        Hide();
    }

    public void OnNoButtonClicked()
    {
        _onResult?.Invoke(false);
        Hide();
    }
    


    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupSetting>("Prefab/UI/PopupPrefabs/PopupSetting" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupSetting;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup4.transform,
                        false);

                    if (completed != null)
                    {
                        completed();
                    }
                }
            };

        }
        else        
        {
            if (completed != null)
            {
                completed();
            }
        }
    }

    public static void Show()//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI();
        });

    }

    public static void Hide()
    {
        if (GameStatic.IS_ANIMATING ) return;

        Instance.Disappear();
    }
    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public void Disappear()
    {
        //Background.gameObject.SetActive(false);
        base.Disappear(()=>{
            Panel.gameObject.SetActive(false);
        });
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void NextStep(object value = null)
    {
    }
    #endregion

}