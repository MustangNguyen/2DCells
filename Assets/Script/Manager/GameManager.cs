using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Lean.Pool;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public CinemachineVirtualCamera virtualCamera;
    public int maximumEnemies = 50;
    public Mutation mutation;
    public CellGun cellGun1;
    public CellGun cellGun2;
    [Space(10)]
    [Header("Game UI")]
    public MutationHealthBar healthBar;
    public Slider xpBar;
    public TextMeshProUGUI currentLvText;
    public int currentLv = 0;
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
    private void Init()
    {
        // spawn player's mutation here
        mutation = LeanPool.Spawn(DataManager.Instance.listMutation[0]);
        EnemySpawner.Instance.playerPosition = mutation.transform;
        virtualCamera.Follow = mutation.transform;
        AudioManager.Instance.StartNormalBattleHematos();
        xpRequire = DataManager.Instance.Data.listIngameLevelConfig[currentLv].xpRequire;
        
    }
    public void OnPauseClick()
    {
        if (!isPause)
        {
            gameStateMachine.ChangeState(new GameStatePause());
            PopupPauseGamePlay.Show();
        }
        else
        {
            gameStateMachine.ChangeState(new GameStatePlay());
        }
    }
    public void OnObsCollect(int XpObs)
    {
        int surplus = 0;
        while (surplus >= 0)
        {
            surplus = XpObs - (xpRequire - currentXp);
            if (surplus < 0)
            {
                currentXp += XpObs;
            }
            else
            {
                XpObs -= xpRequire - currentXp;
                currentXp += xpRequire - currentXp;
                currentLv++;
                currentLvText.text = currentLv.ToString();
                xpRequire = DataManager.Instance.Data.listIngameLevelConfig[currentLv].xpRequire;
                currentXp = 0;
            }
        }
        xpBar.value = (float)currentXp/xpRequire;
    }
}
[Serializable]
public class IngameLevelConfigsOOP
{
    public int inGameLv;
    public int xpRequire;
}
