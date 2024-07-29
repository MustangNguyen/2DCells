using UnityEngine;

public class PlayerState : State
{
    protected Mutation mutation;
    public PlayerState(Mutation mutation){
        this.mutation = mutation;
    }
    
}
