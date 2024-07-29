using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleScorePair : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public int score = 0;
    private Vector2 lastRect;
    private void Start()
    {
        lastRect = rectTransform.sizeDelta;
        gameObject.SetActive(false);
    }
    public void StartAnimation()
    {
        rectTransform.sizeDelta = new Vector2(lastRect.x + 1500f, lastRect.y);
        gameObject.SetActive(true);
        EffectManager.Instance.RectXDurationYParabolaSpeed(rectTransform, lastRect - rectTransform.sizeDelta, duration);
        Debug.Log(lastRect - rectTransform.sizeDelta);
    }
    public void StartCounting(int score,float duration){
        this.score = score;
        StartCoroutine(IEScaleRect(this.score,duration));

    }
    private IEnumerator IEScaleRect(int score, float duration)
    {
        float timeElapse = 0;
        while (duration > timeElapse)
        {
            timeElapse += Time.deltaTime;
            scoreText.text = ((int)Mathf.Lerp(0, score, Mathf.Clamp01(timeElapse / duration))).ToString();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
