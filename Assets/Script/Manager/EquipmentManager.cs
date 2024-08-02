using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public SelectItem currentSelectItem;
    public GunItem gunItem;
    public UserSetEquipmentInfor currentSet;
    public MutationItem currentMutation;
    public GunItem currentGun1;
    public GunItem currentGun2;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI critRate;
    [SerializeField] private TextMeshProUGUI critMultiple;
    [SerializeField] private TextMeshProUGUI bulletName;
    [SerializeField] public string gunOwnedId;
    [SerializeField] private TextMeshProUGUI loadoutButtonText; 

    
    private void Start()
    {
        currentSet = DataManager.Instance.UserData.usersetEquipmentInfor[0];
        UpdateLoadOutUI();
    }
    public void ShowItemInfor()
    {
        CellGunOOP gun = DataManager.Instance.Data.listGun.Find(x=>x.gunId == ((GunItem)currentSelectItem).cellgunInfomation.gunId) ;
        var bullet = DataManager.Instance.Data.listBullet.Find(x => x.bulletId == gun.bulletId);
        gunName.text = $"Name: {gun.gunName}";
        fireRate.text = $"Fire rate: {gun.fireRate}";
        accuracy.text = $"Accuracy: {gun.accuracy}";
        critRate.text = $"Crite rate: {gun.criticalRate}";
        critMultiple.text = $"Crit damage: {gun.criticalMultiple}";
        bulletName.text = $"Bullet: {bullet.bulletName}";
    }
    public void OnClickBackToMenu()
    {
        int loadoutCount = DataManager.Instance.UserData.usersetEquipmentInfor.Count;
        for(int i = 0;i<DataManager.Instance.UserData.usersetEquipmentInfor.Count;i++){
            NetworkManager.Instance.PostUpdateUserEquipment(DataManager.Instance.UserData.usersetEquipmentInfor[i], (data) =>
            {
                loadoutCount--;
                if (loadoutCount==0){
                    SceneLoadManager.Instance.LoadScene(SceneName.MainMenu, false);
                }
            }, (data) =>{});

        }
    }
    public void UpdateLoadOutUI(){
        currentMutation.mutationInfomation = DataManager.Instance.UserData.userMutationInfor.Find(x=>x.ownerShipId == currentSet.mutationOwnershipId);
        currentMutation.InitIcon();
        currentGun1.cellgunInfomation = DataManager.Instance.UserData.userGunInformation.Find(x=>x.ownerShipId == currentSet.gunOwnershipId1);
        currentGun1.InitIcon();
        currentGun2.cellgunInfomation = DataManager.Instance.UserData.userGunInformation.Find(x=>x.ownerShipId == currentSet.gunOwnershipId2);
        currentGun2.InitIcon();
        loadoutButtonText.text = "Loadout " + currentSet.userEquipmentId[currentSet.userEquipmentId.Length - 1];

    }
    public void OnChangeCurrentItem(SelectItem incomeSelectItem)
    {
        if (incomeSelectItem == null)
        {
            currentSelectItem?.IsChoosing(false);
            currentSelectItem = null;
        }
        else
        {
            if (currentSelectItem == null)
            {
                currentSelectItem = incomeSelectItem;
                currentSelectItem.IsChoosing(true);
            }
            else
            {
                currentSelectItem.IsChoosing(false);
                currentSelectItem = incomeSelectItem;
                currentSelectItem.IsChoosing(true);
            }
            if (currentSelectItem is GunItem)
                ShowItemInfor();
        }
    }
}
