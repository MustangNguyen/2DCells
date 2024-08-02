using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public abstract class SelectItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] protected Image icon;
    [SerializeField] protected SpriteAtlas itemSpriteAtlas;
    [SerializeField] protected Button button;
    [SerializeField] public Image selectedBorder;
    [SerializeField] public bool isInteractable = true;
    protected float holdDuration = 1f;
    protected bool isHolding = false;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable) return;
        isHolding = true;
        float timeElapse = 0;
        if (eventData.button == PointerEventData.InputButton.Left)
            StartCoroutine(IEWaitForHoldDuration(true));
        if (eventData.button == PointerEventData.InputButton.Right)
            StartCoroutine(IEWaitForHoldDuration(false));
        IEnumerator IEWaitForHoldDuration(bool isLeft)
        {
            while (isHolding)
            {
                if (timeElapse >= holdDuration)
                {
                    if (isLeft)
                    {
                        OnLeftHoldChosen();
                    }
                    else
                    {
                        OnRightHoldChosen();
                    }
                    isHolding = false;
                }
                yield return null;
                timeElapse += Time.deltaTime;
            }
        }
    }
    public virtual void IsChoosing(bool IsChoosing)
    {
        selectedBorder.gameObject.SetActive(IsChoosing);
    }
    public virtual void OnLeftHoldChosen()
    {

    }
    public virtual void OnRightHoldChosen()
    {

    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!isInteractable) return;
        isHolding = false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;
    }
}