using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTable : MonoBehaviour
{
    [Header("Slot Prefab")]
    [SerializeField] GunItem gunItemPrefab;
    [SerializeField] MutationItem mutationItemPrefab;
    [Space(10)]
    [Header("Properties")]
    [SerializeField] private EquipmentTab currentEquipmentTab = EquipmentTab.Weapon;
    [SerializeField] private RectTransform itemHolder;
    [SerializeField] private List<Tab> itemTabs;
    [SerializeField] private List<SelectItem> itemsToShow;
    [SerializeField] private List<GunItem> gunItems;
    [SerializeField] private List<MutationItem> mutationItems;
    public SelectItem currentItem;
    private void Awake() {
        foreach(var item in itemTabs){
            item.gameObject.SetActive(false);
            item.inventoryTable = this;
            item.gameObject.SetActive(true);
        }
    }
    private void Start() {
        // for (int i = 0; i < 32; i++)
        // {
        //     var selectItem = LeanPool.Spawn(gunItemPrefab,itemHolder);
        //     itemsToShow.Add(selectItem);
        //     LeanPool.Despawn(selectItem);
        // }
        InitWeaponSlots();
        InitMutationSlots();
        foreach(var item in gunItems){
            itemsToShow.Add(LeanPool.Spawn(item,itemHolder));
        }
    }
    public void OnChangeCurrentItem(SelectItem incomeSelectItem){

    }
    private void InitWeaponSlots(){
        var userGunInformations = DataManager.Instance.UserData.userGunInformation;
        for (int i = 0; i < userGunInformations.Count; i++)
        {
            var gunItem = LeanPool.Spawn(gunItemPrefab,itemHolder);
            gunItem.cellgunInfomation = userGunInformations[i];
            gunItem.InitIcon();
            gunItems.Add(gunItem);
        }
        if(userGunInformations.Count<24){
            for (int i = 0; i < 24 - userGunInformations.Count; i++)
            {
                var gunItem = LeanPool.Spawn(gunItemPrefab,itemHolder);
                gunItem.cellgunInfomation = null;
                gunItem.InitIcon();
                gunItems.Add(gunItem);
            }
            // for(int i =0;i< 24 - userGunInformations.Count;i++)
            //     LeanPool.Despawn(gunItems[i+userGunInformations.Count]);
        foreach(var item in gunItems)
            LeanPool.Despawn(item);
        }
                
    }
    private void InitMutationSlots(){
        var userMutationInformations = DataManager.Instance.UserData.userMutationInfor;
        for (int i = 0; i < userMutationInformations.Count; i++)
        {
            var mutationItem = LeanPool.Spawn(mutationItemPrefab,itemHolder);
            mutationItem.mutationInfomation = userMutationInformations[i];
            mutationItem.InitIcon();
            mutationItems.Add(mutationItem);
        }
        if(userMutationInformations.Count<24){
            for (int i = 0; i < 24 - userMutationInformations.Count; i++)
            {
                var mutationItem = LeanPool.Spawn(mutationItemPrefab,itemHolder);
                mutationItem.mutationInfomation = null;
                mutationItem.InitIcon();
                mutationItems.Add(mutationItem);
            }
            // for(int i =0;i< 24 - userMutationInformations.Count;i++)
            //     LeanPool.Despawn(gunItems[i+userGunInformations.Count]);
        foreach(var item in mutationItems)
            LeanPool.Despawn(item);
        }
                
    }
    public void ChangeCurrentTab(EquipmentTab incomeEquipmentTab){
        if(incomeEquipmentTab == currentEquipmentTab) return;
        currentEquipmentTab = incomeEquipmentTab;
        EquipmentManager.Instance.OnChangeCurrentItem(null);
        switch (currentEquipmentTab)
        {
            case EquipmentTab.Weapon:
                SwitchToWeaponTab();
            break;
            case EquipmentTab.Mutation:
                SwitchToMutationTab();
            break;
            default:
            break;
        }
    }
    public void SwitchToWeaponTab(){
        foreach(var item in itemsToShow){
            LeanPool.Despawn(item);
        }
        itemsToShow.Clear();
        foreach(var item in gunItems){
            itemsToShow.Add(LeanPool.Spawn(item,itemHolder));
        }
    }
    public void SwitchToMutationTab(){
        foreach(var item in itemsToShow){
            LeanPool.Despawn(item);
        }
        itemsToShow.Clear();
        foreach(var item in mutationItems){
            itemsToShow.Add(LeanPool.Spawn(item,itemHolder));
        }

    }
}
