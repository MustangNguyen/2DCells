using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColletionManager : Singleton<ColletionManager>
{
    //CharacterItem
    public CharcaterItem charcaterItem;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<CharcaterItem> charcaterItems =  new();
    [SerializeField] private List<EnemyCellOOP> enemyCellOops = new();

    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI faction;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI moveSpeed;
    [SerializeField] private TextMeshProUGUI amor;
    [SerializeField] private TextMeshProUGUI amorType;
    [SerializeField] private TextMeshProUGUI shield;
    [SerializeField] private TextMeshProUGUI shieldPoint;

    public void Start()
    {
        enemyCellOops = DataManager.Instance.Data.listEnemies;
        for(int i = 0; i < enemyCellOops.Count; i++)
        {
            var enemy = Instantiate(charcaterItem, itemHolder);
            enemy.InitIcon(enemyCellOops[i]);  
               charcaterItems.Add(enemy);
            var button = enemy.GetComponent<Button>();
        }
    }
    public void OnClickShowInfor(string id)
    {
        Debug.Log(id);
        var enemy = enemyCellOops.Find(x=>x.enemyId == id);
        name.text = $"Name: {enemy.enemyName}";
        faction.text = $"Faction: {enemy.faction}";
        hp.text = $"HP: {enemy.hp}";
        damage.text = $"Damage: {enemy.bodyDamage}";
        moveSpeed.text = $"Movespeed: {enemy.moveSpeed}";
        amor.text = $"Amor: {enemy.cellProtection.armorPoint}";
        amorType.text = $"Amor Type: {enemy.cellProtection.armorType}";
        shield.text = $"Shield: {enemy.cellProtection.shieldType}";
        shieldPoint.text = $"Shield Point: {enemy.cellProtection.shieldPoint}";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
