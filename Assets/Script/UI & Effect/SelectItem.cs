using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public abstract class SelectItem : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler {
    [SerializeField] protected Image icon;
    [SerializeField] protected SpriteAtlas itemSpriteAtlas;
    [SerializeField] protected Button button;
    [SerializeField] public Image selectedBorder;
    protected float holdDuration = 1f;
    protected bool isHolding = false;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        float timeElapse = 0;
        if(eventData.button == PointerEventData.InputButton.Left)
            StartCoroutine(IEWaitForHoldDuration(true));
        if(eventData.button == PointerEventData.InputButton.Right)
            StartCoroutine(IEWaitForHoldDuration(false));        
        IEnumerator IEWaitForHoldDuration(bool isLeft){
            while(isHolding){
                if(timeElapse>=holdDuration){
                    if(isLeft){
                        Debug.Log("Hold left");
                    }else{
                        Debug.Log("Hold right");
                    }
                    isHolding = false;
                }
                yield return null;
                timeElapse+=Time.deltaTime;
            }
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }
}