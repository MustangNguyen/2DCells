using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCell : CellsBase
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    private bool isMoving = true;
    private float shipAngle = 0f;
    public float rotationInterpolation = 0.4f;
    private void FixedUpdate() {
        isMoving = true;
        if(InputManager.Instance.GetArrowButton() == Vector3.zero){
            isMoving = false;
        }
        PlayerMovement();
        PlayerRotation();
    }
    public void PlayerRotation(){
        Vector2 lookDir = InputManager.Instance.GetArrowButton();
        lookDir.x *= -1;
        if (isMoving)
        {
            shipAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        }

        if (rigidbody2d.rotation <= -90 && shipAngle >= 90){
            rigidbody2d.rotation += 360;
            rigidbody2d.rotation = Mathf.Lerp(rigidbody2d.rotation, shipAngle, rotationInterpolation);
        }

        if (rigidbody2d.rotation >= 90 && shipAngle <= -90){
            rigidbody2d.rotation -= 360;
            rigidbody2d.rotation = Mathf.Lerp(rigidbody2d.rotation, shipAngle, rotationInterpolation);
        }else
        {
            rigidbody2d.rotation = Mathf.Lerp(rigidbody2d.rotation, shipAngle, rotationInterpolation);
        }
    }
    private void PlayerMovement(){
        Vector2 moveDirection = InputManager.Instance.GetArrowButton();
        rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        //rigidbody2d.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
    void GetRotation()
    {
        
    }
}
