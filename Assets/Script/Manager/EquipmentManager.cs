using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public GunItem gunItem;
    [SerializeField] public string bulletId;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<GunItem> gunItems = new();
    [SerializeField] private List<UserGunInformation> userGunInformations = new();
    
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI critRate;
    [SerializeField] private TextMeshProUGUI critMultiple;
    [SerializeField] private TextMeshProUGUI bulletName;

    
    private void Start()
    {
        AudioManager.Instance.StartMainMenuBackGround();
        userGunInformations = DataManager.Instance.Data.userGunInformation;
        for (int i = 0; i < userGunInformations.Count; i++)
        {
            var gunSprite = Instantiate(gunItem,itemHolder);
            gunSprite.InitIcon(userGunInformations[i]);
            //Debug.Log(userGunInformation[i].gunId);
            gunItems.Add(gunSprite);
            var button = gunSprite.GetComponent<Button>();
        }
    }
    public void OnClickShowInfor(string id)
    {
        var gun = DataManager.Instance.Data.listGun.Find(x=>x.gunId ==id);
        Debug.Log(bulletId);
        var bullet = DataManager.Instance.Data.listBullet.Find(x => x.bulletId == bulletId);
        gunName.text = $"Name: {gun.gunName}";
        fireRate.text = $"Fire rate: {gun.fireRate}";
        accuracy.text = $"Accuracy: {gun.accuracy}";
        critRate.text = $"Crite rate: {gun.criticalRate}";
        critMultiple.text = $"Crit damage: {gun.criticalMultiple}";
        Debug.Log(bullet);
        bulletName.text = $"Bullet: {bullet.bulletName}";
    } 
    public void OnClickBackToMenu()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu);
    }
}
