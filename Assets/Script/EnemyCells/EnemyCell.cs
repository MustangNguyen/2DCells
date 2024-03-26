using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCell : CellsBase
{
    [SerializeField] private Rigidbody2D rigidbody;

    protected override void Start() {
        UpdateManager.Instance.AddCellsToPool(this);
        UpdateManager.Instance.transformPoolCount++;
    }
    public void CellUpdate(){
        movement();
    }
    public void movement(){
        Vector3 moveDirection = GameManager.Instance.playerPosition.transform.position - transform.position;
        Vector3.Normalize(moveDirection);
        rigidbody.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
    }
}
