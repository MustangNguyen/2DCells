using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusState : State
{
    public int damagePerTick = 0;
    public int stack = 0;
    protected int maxStack = UNLIMIT_STATUS_STACK;
    public float statusTimeLeft = 0;
    protected float timeBetweenTick = TIME_BETWEEN_STATUS_TICK;
    protected EnemyCell enemyCell;
    protected Mutation mutation;
    public StatusState(CellsBase cellsBase)
    {
        if(cellsBase.GetType() == enemyCell.GetType()){
            enemyCell = (EnemyCell)cellsBase;
        }
        else{
            mutation = (Mutation)cellsBase;
        }
    }
}
