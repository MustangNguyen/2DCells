using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hellmade.Sound;
using Lean.Pool;
public class PopupChoosePowerUp : Popups
{
    public static PopupChoosePowerUp Instance;
    [SerializeField] private PowerUpCard powerUpCardPrefab;
    [SerializeField] private Transform cardHolder;
    private Action<bool> _onResult;
    public List<PowerUpData> listPowerUpToShow;
    void InitUI()
    {
        foreach(Transform child in cardHolder){
            // LeanPool.Despawn(child.gameObject);
            Destroy(child.gameObject);
        }
        Frame.color = new Color(0,0,0,0);
        listPowerUpToShow = new();
        for (int i = 0; i < GameManager.Instance.maxCardToChoose; i++)
        {
            bool isChosen = false;
            while (!isChosen)
            {
                int random = UnityEngine.Random.Range(0, GameManager.Instance.listPowerUpDatas.Count);
                PowerUpData powerUpData =GameManager.Instance.listPowerUpDatas[random];
                if(!listPowerUpToShow.Exists(x=>x.id == powerUpData.id)){
                    listPowerUpToShow.Add(powerUpData);
                    isChosen = true;
                }
            }
            //PowerUpCard cardSpawned = LeanPool.Spawn(powerUpCardPrefab,cardHolder);
            PowerUpCard cardSpawned = Instantiate(powerUpCardPrefab,cardHolder);
            cardSpawned.InitCard(listPowerUpToShow[i],this);
        }
    }

    


    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupChoosePowerUp>("Prefab/UI/PopupPrefabs/PopupChoosePowerUp" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupChoosePowerUp;
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