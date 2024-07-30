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
    [SerializeField] private UserSetEquipmentInfor userSetEquipmentInfor = new();
    [SerializeField] public UserGunInformation equipmentSlot1 = new();
    [SerializeField] public UserGunInformation equipmentSlot2 = new();
    void Start()
    {
        userSetEquipmentInfor = DataManager.Instance.UserData.usersetEquipmentInfor[0];
        equipmentSlot1 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId2);
        cellgun1 = DataManager.Instance.listGun.Find(x=>x.gunId == equipmentSlot1.gunId);
        cellgun2 = DataManager.Instance.listGun.Find(x=>x.gunId == equipmentSlot2.gunId);
        CellGun checkIsfirtgun1 = Instantiate(cellgun1,slot1.transform);
        CellGun checkIsfirtgun2 = Instantiate(cellgun2, slot2.transform);
        checkIsfirtgun1.isFirstGun = true;
        checkIsfirtgun2.isFirstGun = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
