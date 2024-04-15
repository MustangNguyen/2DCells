using System.Collections;
using UnityEngine;
using Lean.Pool;
using static GameStatic;
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] protected int damage = 20;
    [SerializeField] protected Elements elements;
    [SerializeField] protected float timeExist = 2f;
    [SerializeField] protected bool isProjectile = true;
    [SerializeField] protected Rigidbody2D rigidbody2d;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public Elements Elements {get => elements;}
    protected virtual void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        if(rigidbody2d == null){
            rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody2d.gravityScale = 0;
    }
    public void BulletMovement()
    {

    }
    private void Start() {

    }

    public virtual void SetBullet(Transform gunPosition, float accuracy)
    {
        float spreadAngle = GUN_MAX_SPREAD_ANGLE - GUN_MAX_SPREAD_ANGLE / 100 * accuracy;
        float randomAngle = Random.Range(-spreadAngle, spreadAngle + 1);
        Vector2 bulletDirection = InputManager.Instance.mouseWorldPosition - gunPosition.position;
        Quaternion quaternion = Quaternion.Euler(0f,0f,randomAngle);
        bulletDirection = quaternion * bulletDirection;
        bulletDirection.Normalize();
        transform.rotation = quaternion * gunPosition.rotation;
        rigidbody2d.AddForce(bulletDirection * bulletSpeed,ForceMode2D.Impulse);
        LeanTween.delayedCall(timeExist, () =>
        {
            LeanPool.Despawn(gameObject);
        });
    }
}
