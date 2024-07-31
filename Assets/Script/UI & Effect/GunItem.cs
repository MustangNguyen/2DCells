using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GunItem : SelectItem
{
    public UserGunInformation cellgunInfomation;
    [SerializeField] public string bulletId;


    //EquipedGun Parameters

    public void InitIcon()
    {
        if(cellgunInfomation == null){
            selectedBorder.enabled = false;
            icon.sprite = itemSpriteAtlas.GetSprite("off2");
            icon.color = Color.gray;
        }
        else{
            // Sprite sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
            var gunData = DataManager.Instance.Data.listGun.Find(x => x.gunId == cellgunInfomation.gunId);
            bulletId = gunData.bulletId;
            icon.sprite = itemSpriteAtlas.GetSprite(cellgunInfomation.gunId);
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
