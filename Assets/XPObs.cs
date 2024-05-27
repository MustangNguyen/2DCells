using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class XPObs : MonoBehaviour
{
    [SerializeField] Collision2D collision;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] int xPContain = 0;
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float acceleration = 0.01f;
    [SerializeField] float fadeTime = 0.3f;
    [SerializeField] float radius = 1f;
    [SerializeField] float force = 1f;
    [SerializeField] LayerMask layerMask;
    public bool isPulling = false;

    private void Start()
    {

    }
    private void OnEnable() {
        gameObject.layer = LayerMask.NameToLayer("Obs");
        Collider2D []objects = Physics2D.OverlapCircleAll(transform.position,radius,layerMask);
        for(int i = 0;i<objects.Length;i++){
            Vector2 direction = (objects[i].transform.position-transform.position).normalized;
            var pulse = objects[i].GetComponent<Rigidbody2D>();
            pulse.AddForce(direction*force,ForceMode2D.Impulse);
        }
    }
    public void StartMovement()
    {
        isPulling = true;
        StartCoroutine(IEOnObsMove());
    }
    public void OnConsumption()
    {
        Vector2 temp = transform.localScale;
        Color color = model.color;
        LeanTween.value(gameObject, 0, 1, fadeTime).setOnStart(() =>
        {
            isPulling = false;
        }).setOnUpdate((float value) =>
        {
            transform.localScale = temp * (value + 2);
            model.color = new Color(model.color.r, model.color.g, model.color.b, 1f - value);
        }).setOnComplete(() =>
        {
            moveSpeed = 0;
            LeanPool.Despawn(this);
            transform.localScale = temp;
            model.color = color;
        });
    }
    public IEnumerator IEOnObsMove()
    {
        while (isPulling)
        {
            yield return new WaitForFixedUpdate();
            Vector2 moveDirection = (GameManager.Instance.mutation.transform.position - transform.position).normalized;
            rb.velocity = moveDirection * (moveSpeed += acceleration);
        }
        rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnConsumption();
            GameManager.Instance.OnObsCollect(xPContain);
        }
    }
}
