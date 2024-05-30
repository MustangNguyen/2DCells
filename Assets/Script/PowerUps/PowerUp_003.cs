using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_003 : PowerUp
{
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
    }

    protected override void OnFire()
    {
        throw new System.NotImplementedException();
    }
}
