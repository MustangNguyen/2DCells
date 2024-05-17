using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Lean.Pool;
using UnityEditor.Media;
using System.ComponentModel.Design;

public class GameManager : Singleton<GameManager>
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerPosition;
    public int maximumEnemies = 50;
    public CellGun cellGun1;
    public CellGun cellGun2;
    public MutationHealthBar healthBar;
    public StateMachine gameStateMachine;
    public bool isPause = false;
    private void Start()
    {
        Init();
        gameStateMachine.ChangeState(new GameStatePlay());
    }
    private void Init(){
        // spawn player's mutation here
        Mutation mutation = LeanPool.Spawn(DataManager.Instance.listMutation[0]);
        EnemySpawner.Instance.playerPosition = mutation.transform;
        //
        virtualCamera.Follow = mutation.transform;
        playerPosition = mutation.transform;
    }
    public void OnPauseClick(){
        if(!isPause){
            gameStateMachine.ChangeState(new GameStatePause());
            PopupPauseGamePlay.Show();
        }
        else{
            gameStateMachine.ChangeState(new GameStatePlay());
        }
    }
}
