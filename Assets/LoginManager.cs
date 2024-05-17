using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Common;
using System;

public class LoginManager : Singleton<LoginManager>
{
    public TMP_InputField userEmail;
    public TMP_InputField userPassword;
    public Transform loginPanel;
    public TextMeshProUGUI notification;
    public RectTransform confirmPassword;
    public Button submitBtn;
    public Button startBtn;
    public Transform signUpPanel;
    public Button backButton;
    private Vector2 sizeOftextField;
    private float sizeOfNotification;
    private bool isOnTransform = false;
    private bool isSignIn = true;

    private void Start() {
        Init();
    }
    private void Init(){
        userEmail.text = "qwerty@gmail.com";
        userPassword.text = "Abc@123";
        RectTransform rect = (RectTransform)(userPassword.transform);
        sizeOftextField = new Vector2(rect.sizeDelta.x,rect.sizeDelta.y);
        sizeOfNotification = notification.fontSize;
    }
    public void OnSubmitBtnClick()
    {
        if(isOnTransform) return;
        if(isSignIn){
            submitBtn.interactable = false;
            TextMeshProUGUI logInText = submitBtn.GetComponentInChildren<TextMeshProUGUI>();
            logInText.color = new Color(1, 1, 1, 0.5f);
            notification.text = "";
            NetworkManager.Instance.PostRequestLogin(new UserLogin(userEmail.text, userPassword.text), () =>
            {
                loginPanel.gameObject.SetActive(false);
                startBtn.gameObject.SetActive(true);
            }, () =>
            {
                submitBtn.interactable = true;
                logInText.color = new Color(1, 1, 1, 1);
                notification.text = " Can not connect to server";

            }, () =>
            {
                submitBtn.interactable = true;
                logInText.color = new Color(1, 1, 1, 1);
                notification.text = " Email or password is incorrect";

            });
        }
        else
        {

        }
    }
    public void NotificationOutAnim()
    {
        LeanTween.value(gameObject, 0, 1, 0.4f).setOnStart(() =>
        {
            notification.fontSize = 0;
        }).setOnUpdate((float value) =>
        {
            notification.fontSize = sizeOfNotification*value;
        }).setEaseOutQuad().setOnComplete(() =>
        {
        });
    }
    public void NotificationInAnim()
    {
        LeanTween.value(gameObject, 0, 1, 0.4f).setOnStart(() =>
        {
        }).setOnUpdate((float value) =>
        {
            notification.fontSize = sizeOfNotification*value;
        }).setEaseInQuad().setOnComplete(() =>
        {
        });
    }
    public void OnRegisterClick(){
        Vector2 tempSize = sizeOftextField;
        if(notification.gameObject.activeSelf){
            
        }
        LeanTween.value(gameObject, 0, 1, 0.4f).setOnStart(() =>
        {
            notification.text = "";
            isOnTransform = true;
            confirmPassword.sizeDelta = Vector2.zero;
        }).setOnUpdate((float value) =>
        {
            confirmPassword.sizeDelta = tempSize*value;
        }).setEaseOutQuad().setOnComplete(() => {
            isOnTransform = false;
            isSignIn = false;
        });


        signUpPanel.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }
    public void OnBackClick(){
        Vector2 tempSize = sizeOftextField;
        LeanTween.value(gameObject, 1, 0, 0.4f).setOnStart(() =>
        {
            isOnTransform = true;
        }).setOnUpdate((float value) =>
        {
            confirmPassword.sizeDelta = tempSize*value;
        }).setEaseInQuad().setOnComplete(() => {
            isOnTransform = false;
            isSignIn = true;
        });
        signUpPanel.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }
    // private IEnumerator IEInputFieldAnimation(float from,float to,float time,Action onStart =null, Action onFinish = null){
    //     while (time>0){
    //         yield return new WaitForFixedUpdate();
    //     }
    // }
}
