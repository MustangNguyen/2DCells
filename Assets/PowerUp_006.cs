using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_006 : PowerUp
{
    private void Awake() {
        powerUpType = PowerUpType.Instant;
    }
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
    }

    protected override void OnFire()
    {
        Debug.Log("nuke all them");
    }
}
