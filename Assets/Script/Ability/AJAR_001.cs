using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJAR_001 : CellAbility
{
    protected override void Start() {
        base.Start();
        energyConsumption = 25;
        inGameDuration = 3f;
        isRecastable = false;
    }
    protected override void AbilityBehavior()
    {
        SetUpBasicPropertiesOnCast();
        Debug.Log(this.GetType().Name);
        mutation.moveSpeed*=3;
        LeanTween.delayedCall(inGameDuration,()=>{
            mutation.moveSpeed/=3;
        });
    }
}
