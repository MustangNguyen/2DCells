using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    public void OnStartClick(){
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu);
    }
}
