using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GunItem : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private SpriteAtlas gun;
    [SerializeField] Button chooseButton;
    [SerializeField] private string gunId;
    [SerializeField] private string gunOwenredId;
    [SerializeField] public string bulletId;

    public void InitIcon(UserGunInformation cellgun)
    {
        Sprite sprite = gun.GetSprite(cellgun.gunId);
        gunId = cellgun.gunId;
        gunOwenredId = cellgun.ownerShipId;
        var gunData = DataManager.Instance.Data.listGun.Find(x => x.gunId == gunId);
        bulletId = gunData.bulletId;
        icon.sprite = sprite;
    }
    public void OnClick()
    {
       EquipmentManager.Instance.OnClickShowInfor(gunId);
       EquipmentManager.Instance.bulletId = bulletId;
    }
}
