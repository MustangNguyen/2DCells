using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public void LoadScene(SceneName sceneName){
        SceneManager.LoadScene(sceneName.ToString());
    }
}
public enum SceneName{
    MainMenu,
    Launch,
    GamePlay,
}
