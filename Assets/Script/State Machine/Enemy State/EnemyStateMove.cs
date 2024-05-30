using UnityEngine;

public class EnemyStateMove : EnemyState
{
    public EnemyStateMove(EnemyCell enemyCell) : base(enemyCell)
    {
        enemyCell.moveSpeed = enemyCell.defaultMoveSpeed;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
