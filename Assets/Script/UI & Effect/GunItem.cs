using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GunItem : SelectItem
{
    public UserGunInformation cellgunInfomation = null;
    [SerializeField] public string bulletId;


    //EquipedGun Parameters

    public void InitIcon()
    {
        if(cellgunInfomation == null){
            selectedBorder.gameObject.SetActive(false);
            // icon.sprite = itemSpriteAtlas.GetSprite("off2");
            icon.color = Color.gray;
        }
        else{
            // Sprite sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
            var gunData = DataManager.Instance.Data.listGun.Find(x => x.gunId == cellgunInfomation.gunId);
            bulletId = gunData.bulletId;
            icon.color = Color.white;
            icon.sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
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
        if(cellgunInfomation.gunId!=""&&cellgunInfomation.gunId!=null)
            EquipmentManager.Instance.OnChangeCurrentItem(this);
        else
            EquipmentManager.Instance.OnChangeCurrentItem(null);
    }
    public override void OnLeftHoldChosen()
    {
        base.OnLeftHoldChosen();
        EquipmentManager.Instance.currentSet.gunOwnershipId1 = cellgunInfomation.ownerShipId;
        if(EquipmentManager.Instance.currentSet.gunOwnershipId2 == cellgunInfomation.ownerShipId)
            EquipmentManager.Instance.currentSet.gunOwnershipId2 = null;
        EquipmentManager.Instance.UpdateLoadOutUI();
    }
    public override void OnRightHoldChosen()
    {
        base.OnRightHoldChosen();
        EquipmentManager.Instance.currentSet.gunOwnershipId2 = cellgunInfomation.ownerShipId;
        if(EquipmentManager.Instance.currentSet.gunOwnershipId1 == cellgunInfomation.ownerShipId)
            EquipmentManager.Instance.currentSet.gunOwnershipId1 = null;
        EquipmentManager.Instance.UpdateLoadOutUI();
    }
    public void AppearEffect(){
        
    }
}
