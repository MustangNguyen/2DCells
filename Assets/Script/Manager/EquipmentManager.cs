using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public SelectItem currentSelectItem;

    public GunItem gunItem;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<GunItem> gunItems = new();
    [SerializeField] private List<UserGunInformation> userGunInformations = new();
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI critRate;
    [SerializeField] private TextMeshProUGUI critMultiple;
    [SerializeField] private TextMeshProUGUI bulletName;
    [SerializeField] public string gunOwnedId;

    
    private void Start()
    {
        // userGunInformations = DataManager.Instance.UserData.userGunInformation;
        // for (int i = 0; i < userGunInformations.Count; i++)
        // {
        //     var gunSprite = Instantiate(gunItem,itemHolder);
        //     gunSprite.InitIcon();
        //     //Debug.Log(userGunInformation[i].gunId);
        //     gunItems.Add(gunSprite);
        //     var button = gunSprite.GetComponent<Button>();
        // }
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
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu,false);
    }

    public void OnChangeCurrentItem(SelectItem incomeSelectItem)
    {
        if(incomeSelectItem == null){
            currentSelectItem?.IsChoosing(false);
            currentSelectItem = null;
        }
        else{
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
            ShowItemInfor();
        }
    }
}
