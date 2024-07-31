using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomUIColor : MonoBehaviour
{
    [SerializeField] public Image backGround;
    [Range(0, 1)]
    [SerializeField] public float colorLight = 1;
    private void Awake()
    {
        backGround = GetComponent<Image>();
    }
    private void Start()
    {
        backGround.color = new Color(UserUIManager.Instance.GetCurrentUIColor().r * colorLight, UserUIManager.Instance.GetCurrentUIColor().g * colorLight, UserUIManager.Instance.GetCurrentUIColor().b * colorLight);
    }

    public void ChangeButtonColor()
    {
        backGround.color = new Color(UserUIManager.Instance.GetCurrentUIColor().r * colorLight, UserUIManager.Instance.GetCurrentUIColor().g * colorLight, UserUIManager.Instance.GetCurrentUIColor().b * colorLight);
    }
}
