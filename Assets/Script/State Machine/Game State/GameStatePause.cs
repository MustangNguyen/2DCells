using UnityEngine;
public class GameStatePause : GameState{
    public GameStatePause(){
        timeScale = 0;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        Time.timeScale = timeScale;
        InputManager.Instance.isOnPauseState = true;
        Debug.Log("game pause");
    }
}