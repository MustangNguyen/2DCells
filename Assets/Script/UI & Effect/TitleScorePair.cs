using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScorePair : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float duration = 1.5f;
    private Vector2 lastRect;
    private void Start() {
        lastRect = rectTransform.sizeDelta;
        gameObject.SetActive(false);
    } 
    public void StartAnimation(){
        // StartCoroutine(IEScaleRect());
        rectTransform.sizeDelta = new Vector2(lastRect.x + 1500f,lastRect.y);
        gameObject.SetActive(true);
        EffectManager.Instance.RectXDurationYParabolaSpeed(rectTransform, lastRect - rectTransform.sizeDelta, duration);
        Debug.Log(lastRect - rectTransform.sizeDelta);
    }
    // private IEnumerator IEScaleRect(){
    //     yield return null;
    // }
}
