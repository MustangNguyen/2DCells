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
        userSetEquipmentInfor = DataManager.Instance.Data.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet);
        equipmentSlot1 = DataManager.Instance.Data.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.Data.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId2);
        cellgun1 = DataManager.Instance.listGun.Find(x =>x.gunId == equipmentSlot1.gunId);
        cellgun2 = DataManager.Instance.listGun.Find(x=>x.gunId == equipmentSlot2.gunId);
        cellgun2.isFirstGun = false;
        cellgun1.isFirstGun = true;
        Instantiate(cellgun1,slot1.transform);
        Instantiate(cellgun2, slot2.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
