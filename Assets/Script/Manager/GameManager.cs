using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPosition;
    public int maximumEnemies = 50;
    public CellGun cellGun1;
    public CellGun cellGun2;
    public Camera currentCamera;
    private void Start() {
        
    }
}
