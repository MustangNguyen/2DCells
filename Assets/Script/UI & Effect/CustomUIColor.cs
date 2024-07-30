using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomUIColor : MonoBehaviour
{
    [SerializeField] public Image backGround;
    private void Awake(){
        backGround = GetComponent<Image>();
    }
    private void Start() {
        backGround.color = UserUIManager.Instance.GetCurrentUIColor();
    }

    public void ChangeButtonColor()
    {
        backGround.color = UserUIManager.Instance.GetCurrentUIColor();
    }
}
