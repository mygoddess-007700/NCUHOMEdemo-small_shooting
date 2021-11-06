using UnityEngine;

public class HeroPistolBullet : Bullet
{
    [Header("Set In Inspector: HeroPistolBullet")]
    public int damage = 1;
    public float speed = 50f;

    protected override void Update() 
    {
        base.Update();
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
}
