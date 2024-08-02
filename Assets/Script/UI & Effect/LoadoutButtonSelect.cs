using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutButtonSelect : MonoBehaviour
{
    public UserSetEquipmentInfor userSetEquipmentInfor;
    public void OnButtonClick(){
        EquipmentManager.Instance.currentSet = userSetEquipmentInfor;
        EquipmentManager.Instance.UpdateLoadOutUI();
    }
}
