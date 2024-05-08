using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPosition;
    public int maximumEnemies = 50;
    public CellGun cellGun1;
    public CellGun cellGun2;
    public Camera currentCamera;
    public MutationHealthBar healthBar;
    public StateMachine gameStateMachine;
    public bool isPause = false;
    private void Start()
    {
    }
    public void OnPauseClick(){
        if(!isPause){
            gameStateMachine.ChangeState(new GameStatePause());
            PopupPauseGamePlay.Show();
            isPause = true;
        }
        else{
            gameStateMachine.ChangeState(new GameStatePlay());
            isPause = false;
        }
    }
}
