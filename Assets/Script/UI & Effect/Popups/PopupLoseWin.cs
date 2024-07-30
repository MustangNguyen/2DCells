using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hellmade.Sound;
using Unity.VisualScripting;

public class PopupLoseWin : Popups
{
    public static PopupLoseWin Instance;
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
    [SerializeField] bool isWin = false;
    [Space(10)]
    [Header("Battle Information")]
    [SerializeField] RectTransform informationPanel;
    [SerializeField] RectTransform informationTitle;
    [SerializeField] TextMeshProUGUI loseWinDecleration;
    [SerializeField] float shakeDuration = 0.3f;
    [SerializeField] float shakeStrength = 1000f;
    [SerializeField] AnimationCurve shakeCurve;
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
    }
    [ContextMenu("Re-init UI")]
    void InitUI(bool isWin)
    {
        // winBanner.rectTransform.anchoredPosition = lastBannerPosition;
        // winBanner.rectTransform.sizeDelta = lastBannerSize;
        this.isWin = isWin;
        informationPanel.gameObject.SetActive(false);
        bannerTextLower.gameObject.SetActive(this.isWin);
        bannerTextUpper.gameObject.SetActive(this.isWin);
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
        if (isWin)
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
        listTitleScorePair[1].StartCounting(GameManager.Instance.TotalCurrentXp(), bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
        listTitleScorePair[2].StartCounting(isWin ? 10000 : 0, bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);
        listTitleScorePair[3].StartCounting(GameManager.Instance.score + GameManager.Instance.TotalCurrentXp() + (isWin ? 10000 : 0), bannerFloatInDuration);
        yield return new WaitForSeconds(bannerFloatInDuration / 4);

        loseWinDecleration.text = isWin ? "Victory" : "You are a loser";
        yield return new WaitForSeconds(1);
        timeElapse = 0;
        bannerFloatInDuration /= 2;
        while (timeElapse < bannerFloatInDuration)
        {
            loseWinDecleration.color = new Color(1, 1, 1, Mathf.Clamp01(timeElapse / bannerFloatInDuration));
            timeElapse += Time.deltaTime;
            loseWinDecleration.fontSize = 72 + 144 * Mathf.Clamp01(1 - timeElapse / bannerFloatInDuration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        // EffectManager.Instance.ShakeCamera();
        loseWinDecleration.color = new Color(1, 1, 1, 1);
        loseWinDecleration.fontSize = 72;
        bannerFloatInDuration *= 2;
        yield return StartCoroutine(Shaking());
    }
    public IEnumerator Shaking()
    {
        float timeElapse = 0;
        Vector3 startPosition = Frame.rectTransform.anchoredPosition;
        while (timeElapse < shakeDuration)
        {
            timeElapse += Time.deltaTime;
            float strength = shakeCurve.Evaluate(Mathf.Clamp01(timeElapse / shakeDuration)) * shakeStrength;
            Frame.rectTransform.anchoredPosition = startPosition + UnityEngine.Random.insideUnitSphere * strength;
            Debug.Log(Frame.rectTransform.anchoredPosition);
            yield return null;
        }
        Frame.rectTransform.anchoredPosition = startPosition;
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

            var loadAsset = Resources.LoadAsync<PopupLoseWin>("Prefab/UI/PopupPrefabs/PopupLoseWin" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupLoseWin;
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

    public static void Show(bool isWin = false)//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI(isWin);
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
