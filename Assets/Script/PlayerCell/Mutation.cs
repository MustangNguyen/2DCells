using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mutation : CellsBase
{
    [SerializeField] private Rigidbody2D playerRigidbody2d;
    [SerializeField] private float pushBackForce = 1f;
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

        if (playerRigidbody2d.rotation <= -90 && shipAngle >= 90){
            playerRigidbody2d.rotation += 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }

        if (playerRigidbody2d.rotation >= 90 && shipAngle <= -90){
            playerRigidbody2d.rotation -= 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }else
        {
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }
    }
    private void PlayerMovement(){
        Vector2 moveDirection = InputManager.Instance.GetArrowButton();
        playerRigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        //playerRigidbody2d.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyCell")
        {
            Vector2 collisionDirection = other.contacts[0].normal.normalized;
            // Debug.Log(collisionDirection * pushBackForce);
            playerRigidbody2d.AddForce(collisionDirection * pushBackForce, ForceMode2D.Force);
        }
    }
    void GetRotation()
    {
        
    }
}

[Serializable]
public class MutationOOP
{
    [Header("Base Stat")]
    public string mutationID;
    public string mutationName;
    public int maxHealth = 200;
    public int maxEnery = 200;
    public CellProtection baseCellProtection;
    public float moveSpeed = 1f;
    public int lv = 1;
    public List<Ability> mutationAbilities;
    public MutationOOP()
    {
        baseCellProtection = new CellProtection();
        mutationAbilities = new List<Ability>();
    }
}