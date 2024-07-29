using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hellmade.Sound;
using Unity.VisualScripting;

public class PopupWin : Popups
{
    public static PopupWin Instance;
    #region DEFINE VARIABLES
    private Action<bool> _onResult;
    #endregion
    [Space(10)]
    [Header("Banner")]
    [SerializeField] Image winBanner;
    [SerializeField] float bannerFloatInDuration = 1f;
    [SerializeField] TextMeshProUGUI bannerTextUpper;
    [SerializeField] TextMeshProUGUI bannerTextLower;
    [SerializeField] float bannerTextWaitDuration = 1f;
    [Space(10)]
    [Header("Battle Information")]
    [SerializeField] RectTransform informationPanel;
    [SerializeField] RectTransform informationTitle;
    [SerializeField] List<TitleScorePair> listTitleScorePair;

    private Vector2 lastBannerPosition;
    private Vector2 lastBannerSize;
    private Vector2 lastInformationTitlePosition;
    #region FUNCTION

    private void Start()
    {
        lastBannerPosition = winBanner.rectTransform.anchoredPosition;
        lastBannerSize = winBanner.rectTransform.sizeDelta;
        lastInformationTitlePosition = informationTitle.anchoredPosition;
        informationTitle.anchoredPosition = new Vector2(informationTitle.anchoredPosition.x, informationTitle.anchoredPosition.y + 500f);
        InitUI();
    }
    [ContextMenu("Re-init UI")]
    void InitUI()
    {
        // winBanner.rectTransform.anchoredPosition = lastBannerPosition;
        // winBanner.rectTransform.sizeDelta = lastBannerSize;
        informationPanel.gameObject.SetActive(false);
        AppearBanner();
    }

    public void AppearBanner()
    {
        StartCoroutine(IEMoveBannerIn());
    }
    public IEnumerator IEMoveBannerIn()
    {
        yield return new WaitForSeconds(0.5f);
        float timeElapse = 0f;
        // EffectManager.Instance.MoveXDurationYParabolaSpeed(winBanner.rectTransform,new Vector3(4000f,0),bannerFloatInDuration);
        bannerFloatInDuration *= 2;
        // yield return new WaitForSeconds(bannerFloatInDuration / 2);
        while (timeElapse < bannerFloatInDuration / 2)
        {
            winBanner.rectTransform.anchoredPosition = new Vector3(lastBannerPosition.x + Mathf.Abs((lastBannerPosition.x - 0) * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / bannerFloatInDuration) - 1, 2))), 0, 0);
            timeElapse += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);

        }
        GameManager.Instance.mutation.gameObject.SetActive(false);
        // timeElapse = bannerFloatInDuration/2;
        yield return new WaitForSeconds(bannerTextWaitDuration);

        while (timeElapse < bannerFloatInDuration)
        {
            winBanner.rectTransform.sizeDelta = new Vector3(lastBannerSize.x, lastBannerSize.y + Mathf.Abs((4000f - lastBannerSize.y) * Mathf.Pow(2 * Mathf.Clamp01(timeElapse / bannerFloatInDuration) - 1, 2)), 0);
            timeElapse += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        bannerFloatInDuration /= 2;
        informationPanel.gameObject.SetActive(true);

        EffectManager.Instance.MoveXDurationYParabolaSpeed(informationTitle, lastInformationTitlePosition - informationTitle.anchoredPosition, 0.2f);

        yield return new WaitForSeconds(Time.deltaTime);
        foreach (var item in listTitleScorePair)
        {
            item.StartAnimation();
            yield return new WaitForSeconds(bannerFloatInDuration / 2);
        }
        listTitleScorePair[0].StartCounting(GameManager.Instance.score, bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
        listTitleScorePair[1].StartCounting(GameManager.Instance.TotalCurrentXp(), bannerFloatInDuration );
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
        listTitleScorePair[2].StartCounting(10000, bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
        listTitleScorePair[3].StartCounting(GameManager.Instance.score + GameManager.Instance.TotalCurrentXp() + 10000, bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
    }
    public void BackToStarChart()
    {
        Debug.Log("button clicked!");
        SceneLoadManager.Instance.LoadScene(SceneName.Campaign);
    }
    #endregion

    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupWin>("Prefab/UI/PopupPrefabs/PopupWin" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupWin;
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
        //if (GameStatic.IS_ANIMATING ) return;
        //Debug.Log("close");
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
        base.Disappear(() =>
        {
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
