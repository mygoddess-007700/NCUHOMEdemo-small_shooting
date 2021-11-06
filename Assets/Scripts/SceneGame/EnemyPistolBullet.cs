using UnityEngine;

public class EnemyPistolBullet : Bullet
{
    [Header("Set In Inspector: EnemyPistolBullet")]
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
