using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_002 : PowerUp
{
    public SpriteRenderer burstSprite;
    protected override void Start()
    {
        base.Start();
    }
    private void OnEnable()
    {
        scanRadius = gameObject.transform.localScale.x / 11;
    }
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
        PowerUpData_FrostCurse powerUpData = (PowerUpData_FrostCurse)GameManager.Instance.listPlayerPowerUpDatas.Find(x => x.id == id);
        modifiedDamage = (int)((float)damage / powerUpData.FrostCurseUpgrades[0].damage * powerUpData.FrostCurseUpgrades[this.lv].damage);
        float currentRadius = powerUpData.FrostCurseUpgrades[this.lv].radius*10;
        gameObject.transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);
        scanRadius = gameObject.transform.localScale.x / 11;
        timeCharge = powerUpData.FrostCurseUpgrades[this.lv].delay;
    }

    protected override void OnFire()
    {
        Debug.Log("scan radius:" + scanRadius);
        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(mutation.transform.position, scanRadius, layerMask);
        for (int i = 0; i < enemyArray.Length; i++)
        {
            EnemyCell enemyCell = enemyArray[i].GetComponent<EnemyCell>();
            enemyCell.TakeDamage(damage, 0);
            enemyCell.SetStatusMachine(PrimaryElement.Ice,damage,1);
        }
        StartCoroutine(IEBursAnimation());
        IEnumerator IEBursAnimation(){
            float duration = 0.3f;
            float currentduration = duration;
            burstSprite.color = new Color(162f/255,1,1,20f/255);
            while(currentduration>0){
                burstSprite.transform.localScale = Vector3.one *(1 - currentduration/duration);
                currentduration -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            StartCoroutine(IEBurstFade());
        }
        IEnumerator IEBurstFade(){
            float duration = 0.6f;
            float currentduration = duration;
            while(currentduration>0){
                burstSprite.color = new Color(162f/255,1,1,20f/255 * (currentduration/duration));
                currentduration -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
    
}
