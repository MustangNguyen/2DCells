using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject slot1;
    [SerializeField] public GameObject slot2;
    public CellGun cellgun1;
    public CellGun cellgun2;
    private string equipmentSet = "735d1359-08f5-4e98-baa1-406a2120373f1";
    [SerializeField] private UserSetEquipmentInfor userSetEquipmentInfor = new();
    [SerializeField] public UserGunInformation equipmentSlot1 = new();
    [SerializeField] public UserGunInformation equipmentSlot2 = new();
    void Start()
    {
        userSetEquipmentInfor = DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet);
        equipmentSlot1 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId2);
        cellgun1 = Instantiate(DataManager.Instance.listGun.Find(x => x.gunId == equipmentSlot1.gunId), slot1.transform);
        cellgun2 = Instantiate(DataManager.Instance.listGun.Find(x => x.gunId == equipmentSlot2.gunId), slot2.transform);
        cellgun2.isFirstGun = false;
        cellgun1.isFirstGun = true;
    }


}
