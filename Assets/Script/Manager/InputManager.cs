using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private Camera worldCamera;

    public Vector3 mouseWorldPosition;
    public Vector3 arrowDirection;
    public bool isMouseClick = false;
    public bool isMouseClick2 = false;
    public Action onFire;
    public Action onFire2;
    public bool isOnPauseState = false;
    private void Update() {
        if(!isOnPauseState){
            GetMousePosition();
            GetArrowButton();
            GetMouseClick();
            GetMouseHold();
        }
    }
    public Vector3 GetMousePosition(){
        mouseWorldPosition = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        return worldCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    public Vector3 GetWASD(){

        return Vector3.zero;
    }
    public Vector3 GetArrowButton(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        arrowDirection = new Vector3(horizontal, vertical, 0f);
        return arrowDirection.normalized;
    }
    public void GetMouseClick(){
        isMouseClick = Input.GetButtonDown("Fire1");
        if(isMouseClick)
            onFire?.Invoke();
        isMouseClick2 = Input.GetButtonDown("Fire2");
        if(isMouseClick2)
            onFire2?.Invoke();
    }
    public void GetMouseHold(){
        bool isMouseHold = Input.GetMouseButton(0);
        if(isMouseHold)
            onFire?.Invoke();
        bool isMouseHold2 = Input.GetMouseButton(1);
        if(isMouseHold2)
            onFire2?.Invoke();
    }
}
