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
    public PrimaryElement primaryElement { get; protected set; } = PrimaryElement.None;
    public SecondaryElement secondaryElement { get; protected set; } = SecondaryElement.None;
    protected EnemyCell enemyCell;
    protected Mutation mutation;
    public StatusState(CellsBase cellsBase)
    {
        if (cellsBase is EnemyCell)
        {
            enemyCell = (EnemyCell)cellsBase;
        }
        else
        {
            mutation = (Mutation)cellsBase;
        }
    }
    public StatusState(StatusState statusState)
    {
        damagePerTick = statusState.damagePerTick;
        stack = statusState.stack;
        maxStack = statusState.maxStack;
        statusTimeLeft = statusState.statusTimeLeft;
        timeBetweenTick = statusState.timeBetweenTick;
        primaryElement = statusState.primaryElement;
        secondaryElement = statusState.secondaryElement;
        enemyCell = statusState.enemyCell;
        mutation = statusState.mutation;
    }
    public override void Enter()
    {
        base.Enter();
        if (this is not StatusStateNormal)
        {
            statusTimeLeft = STATUS_DURATION;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // if(enemyCell!=null)
        //     Debug.Log("status is on enemy");
        // else
        //     Debug.Log("status is on mutation");
        statusTimeLeft -= Time.fixedDeltaTime;
        if (statusTimeLeft <= 0)
            enemyCell?.SetStatusMachine(PrimaryElement.None);
        if (stack <= 0)
            enemyCell?.SetStatusMachine(PrimaryElement.None);
    }
    protected virtual void ResetState()
    {
        damagePerTick = 0;
        stack = 0;
        primaryElement = PrimaryElement.None;

    }
    public int CurrentStatusLevel()
    {
        if (primaryElement != PrimaryElement.None)
            return 1;
        else if (secondaryElement != SecondaryElement.None)
            return 2;
        else
            return 0;
    }
}
