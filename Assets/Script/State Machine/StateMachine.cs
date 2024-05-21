using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string currentStateName;
    [SerializeField] private string currentStateStatusName;
    private State currentState;
    private StatusState currentStatusState;
    public void StateMachineUpdate() {
        currentState?.LogicUpdate();
        currentStatusState?.LogicUpdate();
    }
    public void StateMachineFixedUpdate() {
        currentState?.PhysicsUpdate();
        currentStatusState?.PhysicsUpdate();
    }
    public void ChangeStatusState(StatusState newStatusState){
        if(currentStatusState!=null){
            currentStatusState.Exit();
        }
        currentStatusState = newStatusState;
        currentStatusState.Initialize(this);
        currentStatusState.Enter();
        currentStateStatusName = currentStatusState.ToString();
    }
    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentStateName = currentState.ToString();
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentStateName = currentState.ToString();
    }
    public void ChangeState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentState.LogicUpdate();
        currentStateName = currentState.ToString();
    }
}
