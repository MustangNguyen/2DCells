using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJAR_002 : CellAbility
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
        // mutation.moveSpeed*=10;
        // LeanTween.delayedCall(inGameDuration,()=>{
        //     mutation.moveSpeed/=10;
        // });
    }
}
