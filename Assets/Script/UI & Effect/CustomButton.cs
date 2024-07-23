using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    [SerializeField] public Image backGround;
    private void Start() {
        backGround.color = UserUIManager.Instance.GetCurrentUIColor();
    }

    public void ChangeButtonColor()
    {
        backGround.color = UserUIManager.Instance.GetCurrentUIColor();
    }
}
