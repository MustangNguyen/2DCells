using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusState : State
{
    public int damagePerTick = 0;
    public int stack = 0;
    public int maxStack = UNLIMITED_STATUS_STACK;
    public float statusTimeLeft = 0;
    protected float timeBetweenTick = TIME_BETWEEN_STATUS_TICK;
    protected PrimaryElement primaryElement = PrimaryElement.None;
    protected EnemyCell enemyCell;
    protected Mutation mutation;
    public StatusState(CellsBase cellsBase)
    {
        if(cellsBase is EnemyCell){
            enemyCell = (EnemyCell)cellsBase;
        }
        else{
            mutation = (Mutation)cellsBase;
        }
    }
    public override void Enter()
    {
        base.Enter();
        if(this is not StatusStateNormal){
            statusTimeLeft = STATUS_DURATION;
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
        // if(enemyCell!=null)
        //     Debug.Log("status is on enemy");
        // else
        //     Debug.Log("status is on mutation");
        statusTimeLeft-=Time.fixedDeltaTime;
        if(statusTimeLeft<=0)
            enemyCell?.SetStatusMachine(PrimaryElement.None);
        if (stack <= 0)
            enemyCell?.SetStatusMachine(PrimaryElement.None);
    }
    protected virtual void resetState(){
        damagePerTick = 0;
        stack = 0;
        primaryElement = PrimaryElement.None;

    }
}
