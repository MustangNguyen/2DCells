using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string currentStateName;
    private State currentState;
    public void StateMachineUpdate() {
        currentState?.LogicUpdate();
    }
    public void StateMachineFixedUpdate() {
        currentState?.PhysicsUpdate();
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
