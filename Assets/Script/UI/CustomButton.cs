using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField] private Image backGround;
    private void Start() {
        backGround.color = UserUIManager.Instance.GetCurrentUIColor();
    }
}
