using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateBurn : StatusState
{
    public StatusStateBurn(CellsBase cellsBase,int initDamage, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            damagePerTick = initDamage/10;
            stack+=stackAddIn;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    private void DamageBurnPerTickToEnemy(){
        timeBetweenTick -= Time.fixedDeltaTime;
        if(timeBetweenTick<=0){
            timeBetweenTick += TIME_BETWEEN_STATUS_TICK;
            
        }
    }
}
