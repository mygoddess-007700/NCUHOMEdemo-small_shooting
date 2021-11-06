using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public enum BulletMode{idle, gOut, gInHit, gMiss};
    public abstract int Damage{get; set;}

    [Header("Set Dynamically")]
    public BulletMode mode = BulletMode.idle;
    public Vector2 dir = Vector2.zero;
    public float liveDone = 5f;
    protected float generatedTime = 0;
    protected Rigidbody rigid;

    protected virtual void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
    }

    protected virtual void Start() 
    {
        generatedTime = Time.time;
    }

    protected virtual void Update() 
    {
        if(Time.time-generatedTime > liveDone)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider colld) 
    {
        if(colld.gameObject.tag == "Untagged")
            Destroy(gameObject);    
    }
}
