using UnityEngine;

public class CloseEnemy : Enemy
{
    [Header("Set In Inspector: CloseEnemy")]
    public float speed = 1f;
    public int damage = 1;

    protected override void Update() 
    {
        base.Update();
        mode = eMode.move;

        rigid.velocity = dir * speed;
    }

    void OnCollisionEnter(Collision coll) 
    {
        invincibleDone = Time.time-generatedTime + invincibleDuration;
        if(coll.gameObject.tag == "HeroBullet")
        {
            invincible = true;
            Bullet bullet = coll.gameObject.GetComponent<HeroPistolBullet>();
            health -= bullet.Damage;
            if(health <= 0)
            {
                hero.score += score;
                Destroy(gameObject);
            }
            Destroy(coll.gameObject);
        }
    }
}
