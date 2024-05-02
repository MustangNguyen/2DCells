using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectByChild : MonoBehaviour
{
    #if UNITY_EDITOR
    private void Reset() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect rect = transform.GetChild(0).GetComponent<RectTransform>().rect;
        rectTransform.sizeDelta = new Vector2(rect.width,rect.height);
        
        
    }
    #endif
}
