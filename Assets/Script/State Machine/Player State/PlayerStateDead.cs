using UnityEngine;
public class PlayerStateDead : PlayerState{
    public PlayerStateDead(Mutation mutation) : base(mutation)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        mutation.PlayDeadAnimation();
        mutation.gameObject.SetActive(false);
        EffectManager.Instance.ShakeCamera();
        GameManager.Instance.OnLose();

    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    }
}