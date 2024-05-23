using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Lean.Pool;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerPosition;
    public int maximumEnemies = 50;
    public Mutation mutation;
    public CellGun cellGun1;
    public CellGun cellGun2;
    [Space(10)]
    [Header("Game UI")]
    public MutationHealthBar healthBar;
    public Slider xpBar;
    public int xpRequire;
    public int currentXp;
    [Space(10)]
    [Header("Game State")]
    public StateMachine gameStateMachine;
    public bool isPause = false;
    private void Start()
    {
        Init();
        gameStateMachine.ChangeState(new GameStatePlay());
    }
    private void Init(){
        // spawn player's mutation here
        mutation = LeanPool.Spawn(DataManager.Instance.listMutation[0]);
        EnemySpawner.Instance.playerPosition = mutation.transform;
        virtualCamera.Follow = mutation.transform;
        playerPosition = mutation.transform;
        AudioManager.Instance.StartNormalBattleHematos();
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
[Serializable]
public class IngameLevelConfigsOOP{
    public int inGameLv;
    public int xpRequire;
}
