using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletMode{idle, gOut, gInHit, gMiss};

    [Header("Set In Inspector")]
    public int damage = 1;
    public float speed = 10f;

    [Header("Set Dynamically")]
    public BulletMode mode = BulletMode.idle;
    public Vector2 dir = Vector2.zero;

    private Vector2 flyDirection;
    private Rigidbody rigid;

    protected virtual void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
    }

    protected virtual void Update() 
    {
        if(mode == BulletMode.gOut)
        {
            rigid.velocity = dir * speed;
            transform.parent = null;
        }

    }

    private void destroyBullet()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision coll) 
    {
        
    }

    protected virtual void OnTriggerEnter(Collider colld) 
    {
        
    }
}
