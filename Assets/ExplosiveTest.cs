using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTest : MonoBehaviour
{
    [SerializeField] private float impactField;
    [SerializeField] private float impactForce;
    [SerializeField] private float countdown = 10;
    public LayerMask layerToHit;
    private void Start() {
        explode();
    }
    private void FixedUpdate(){
        countdown-=Time.fixedDeltaTime;
        if(countdown<=0){
            countdown=1;
            explode();
        }
    }
    private void explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,impactField,layerToHit);
        foreach(Collider2D obj in objects){
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            var victim = obj.GetComponent<Rigidbody2D>();
            victim.AddForce(direction*impactForce,ForceMode2D.Impulse);
        }
    }
}
