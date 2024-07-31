using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField] public InventoryTable inventoryTable;
    [SerializeField] private Button button;
    [SerializeField] public EquipmentTab equipmentTab = EquipmentTab.Weapon;
    private void OnEnable() {
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable() {
        button.onClick.RemoveAllListeners();
    }
    public void OnClick(){
        inventoryTable.ChangeCurrentTab(equipmentTab);
    }
}
[Serializable]
public enum EquipmentTab{
    Weapon,
    Mutation,
}
