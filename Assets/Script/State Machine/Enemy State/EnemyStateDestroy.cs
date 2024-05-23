using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDestroy : EnemyState
{
    public EnemyStateDestroy(EnemyCell enemyCell) : base(enemyCell)
    {

    }
    public override void Enter()
    {
        base.Enter();
        enemyCell.OnDead();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SpawnObs(){
        
    }
}
