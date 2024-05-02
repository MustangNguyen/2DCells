using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class UserUIManager : Singleton<UserUIManager>
{
    public List<Color> listUIColor;
    public Color currentUIColor = USER_UI_COLOR_CYAN;
    private void Start() {
        currentUIColor = USER_UI_COLOR_CYAN;
        listUIColor = new List<Color>(){
            USER_UI_COLOR_BLUE,
            USER_UI_COLOR_CYAN
        };
    }
    public void ChangeUIColor(Color color){
        currentUIColor = color;
    }
    public Color GetCurrentUIColor(){
        return currentUIColor;
    }
}
