using UnityEngine;

public class EnemyRocketBullet : Bullet
{
    [Header("Set In Inspector: EnemyRocketBullet")]
    public int damage = 1;
    public float speed = 5f;
    public GameObject heroGO;

    protected override void Start() 
    {
        base.Start();

        FollowCam.POI = this.gameObject;
    }

    protected override void Update() 
    {
        if(Time.time-generatedTime > liveDone)
        {
            FollowCam.POI = heroGO.gameObject;
            Destroy(gameObject);
        }  
        if(mode == BulletMode.gOut)
        {
            rigid.velocity = dir * speed;
            transform.parent = null;
        }    
    }

    public override int Damage 
    {
        get{return damage;}
        set{damage = value;}
    }

    void OnCollisionEnter(Collision coll) 
    {
        if(coll.gameObject.tag == "HeroBullet")
        {
            Destroy(coll.gameObject);
            Destroy(this.gameObject);
        }
    }
}
