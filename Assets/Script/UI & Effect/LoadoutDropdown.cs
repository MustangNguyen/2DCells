using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutDropdown : MonoBehaviour
{
    [SerializeField] private RectTransform dropdownPanel;
    [SerializeField] private RectTransform dropdownContent;
    [SerializeField] private Button loadoutButton;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float maxPanelHeight = 270f;
    private Vector3 maxExpandSize;
    private bool isExpanding = false;
    private bool canClick = true;
    private void Start() {
        InitDropdown();
        maxExpandSize = new Vector3(dropdownPanel.sizeDelta.x, maxPanelHeight);
        canClick = true;
    }
    public void InitDropdown(){
        for(int i = 0;i<DataManager.Instance.UserData.usersetEquipmentInfor.Count;i++){
            var newLoadoutButton = Instantiate(loadoutButton,dropdownContent);
            TextMeshProUGUI loadoutOrder = newLoadoutButton.GetComponentInChildren<TextMeshProUGUI>();
            LoadoutButtonSelect loadoutButtonSelect = newLoadoutButton.GetComponentInChildren<LoadoutButtonSelect>();
            loadoutButtonSelect.userSetEquipmentInfor = DataManager.Instance.UserData.usersetEquipmentInfor[i];
            loadoutOrder.text = "Loadout " + i.ToString();
        }
    }
    public void OnLoadoutListClick(){
        if(!canClick) return;
        canClick = false;
        if(isExpanding){
            GeneralEffectManager.Instance.RectXDurationYParabolaSpeed(dropdownPanel,new Vector3(0,maxExpandSize.y*-1),duration,false,()=>{
                canClick = true;
                isExpanding = false;
            });
        }
        else{
            GeneralEffectManager.Instance.RectXDurationYParabolaSpeed(dropdownPanel,new Vector3(0,maxExpandSize.y),duration,false,()=>{
                canClick = true;
                isExpanding = true;
            });
        }
    }
}
