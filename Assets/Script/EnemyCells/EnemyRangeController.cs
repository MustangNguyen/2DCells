using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    [SerializeField] public float range = 5f;
    [SerializeField] private EnemyGun cellGun;
    private void Start() {
        cellGun.range = range*2;
    }
}
