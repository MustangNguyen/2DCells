using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using JetBrains.Annotations;
public class ToxinSplash : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private float damageInterval = 0.5f;
    [SerializeField] private float timer = 0f;
    public float duration = 2f; 
    private float timerSpawn = 0f; 
    public float targetScale = 25f;

    // [SerializeField] private float despawnTimer = 0f;
    EnemyCell cell;

 
    private void Start()
    {
        StartCoroutine(Destroy());
    }
    private void Update()
    {
        timerSpawn += Time.deltaTime;
        float scale = Mathf.Lerp(0f, targetScale, timerSpawn / duration); 
        transform.localScale = Vector3.one * scale;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "EnemyCell")
        {
            cell = collision.gameObject.GetComponent<EnemyCell>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCell")
        {
            timer += Time.deltaTime;
            cell = collision.gameObject.GetComponent<EnemyCell>();
            if (timer >= damageInterval)
            {
                cell.TakeDamage(damage, 0);
                cell.SetStatusMachine(PrimaryElement.Toxin, 3, 1);
                timer = 0f;
            }
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
