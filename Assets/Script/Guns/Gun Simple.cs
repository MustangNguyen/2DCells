using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.EventSystems;

public class GunSimple : CellGun
{
    protected override void SetFire(){
        base.SetFire();
    }
    protected override IEnumerator OnFire()
    {
        base.OnFire();
        yield return null;
    }
    public override void GunRotation(){
        base.GunRotation();
    }
}
