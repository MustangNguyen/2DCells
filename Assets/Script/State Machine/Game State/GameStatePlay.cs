using UnityEngine;
public class GameStatePlay : GameState{
    public GameStatePlay(){
        timeScale = 1;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        Time.timeScale = timeScale;
        InputManager.Instance.isOnPauseState = false;
        Debug.Log("game play");
    }
}