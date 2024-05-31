using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class ToxinSplash : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public int duration;
    [SerializeField] public int radius;
    EnemyCell cell;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCell")
        {
            cell = collision.gameObject.GetComponent<EnemyCell>();
            StartCoroutine(DoT());
            cell.SetStatusMachine(PrimaryElement.Toxin, damage, 1);
            StartCoroutine(Destroy());
        }
    }
  
    public void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(DoT());
    }
    IEnumerator DoT()
    {
        while (cell.healPoint != 0)
        {
            cell.TakeDamage(30, 0);
            yield return new WaitForSeconds(1.5f);
        }  
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        LeanPool.Despawn(this);
    }
}
