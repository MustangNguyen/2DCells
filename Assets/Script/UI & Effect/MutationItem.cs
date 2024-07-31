using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MutationItem : SelectItem
{
    public UserMutaitonInfor mutationInfomation;


    //EquipedGun Parameters

    public void InitIcon()
    {
        if(mutationInfomation == null){
            selectedBorder.enabled = false;
            icon.sprite = itemSpriteAtlas.GetSprite("off2");
            icon.color = Color.gray;
        }
        else{
            // Sprite sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
            var gunData = DataManager.Instance.Data.listMutations.Find(x => x.mutationID == mutationInfomation.mutationId);
            icon.sprite = itemSpriteAtlas.GetSprite(mutationInfomation.mutationId);
            selectedBorder.enabled = false;
        }
    }

    public void OnClick()
    {
        // EquipmentManager.Instance.bulletId = bulletId;
        // EquipmentManager.Instance.gunOwnedId = gunOwenredId;
        // EquipmentManager.Instance.OnClickShowInfor(gunId);
        // Debug.Log(gunOwenredId);
    }
}
