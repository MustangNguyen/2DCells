using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ABIL_003 : CellAbility
{
    protected override void Start() {
        base.Start();
        energyConsumption = 25;
    }
    protected override void AbilityBehavior()
    {
        Debug.Log("ability 3");
    }
}
