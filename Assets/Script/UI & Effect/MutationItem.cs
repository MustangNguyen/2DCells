using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MutationItem : SelectItem
{
    public UserMutaitonInfor mutationInfomation;


    //EquipedGun Parameters

    public void InitIcon()
    {
        if(mutationInfomation == null){
            selectedBorder.gameObject.SetActive(false);
            // icon.sprite = itemSpriteAtlas.GetSprite("off2");
            icon.color = Color.gray;
        }
        else{
            // Sprite sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
            var gunData = DataManager.Instance.Data.listMutations.Find(x => x.mutationID == mutationInfomation.mutationId);
            icon.color = Color.white;
            icon.sprite = itemSpriteAtlas.GetSprite(mutationInfomation.mutationId);
            selectedBorder.gameObject.SetActive(false);
        }
    }

    public override void IsChoosing(bool IsChoosing){
        base.IsChoosing(IsChoosing);
    }
    public override void OnPointerClick(PointerEventData eventData){
        OnButtonClick();
    }
    public void OnButtonClick()
    {
        if (mutationInfomation.mutationId != null && mutationInfomation.mutationId != "" )
            EquipmentManager.Instance.OnChangeCurrentItem(this);
        else
            EquipmentManager.Instance.OnChangeCurrentItem(null);
    }
}
