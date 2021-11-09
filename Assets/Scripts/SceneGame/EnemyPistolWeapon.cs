using UnityEngine;

public class EnemyPistolWeapon : Weapon
{

    [Header("Set Dynamically: EnemyPistolWeapon")]
    public WeaponMode mode = WeaponMode.idle;

    private SpriteRenderer pistolSRend;
    private Enemy enemy;
    private float generatedTime = 0;

    void Start() 
    {
        generatedTime = Time.time;
        Transform trans = transform.Find("Pistol");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
        pistolSRend = trans.GetComponent<SpriteRenderer>();
        enemy = transform.parent.GetComponent<Enemy>();
    }
    void LateUpdate()
    {
        Direction2D = enemy.Direction2D;
        if(this.gameObject.activeSelf)
        {
            switch(enemy.mode)
            {
                case Enemy.eMode.readyShoot:
                    mode = WeaponMode.readyShoot;
                    BeforeShootingTime();
                    break;
                case Enemy.eMode.shoot:
                    pistolSRend.enabled = true;
                    if(Time.time-generatedTime >= TimeShootNext)
                    {
                        GameObject goBullet = Instantiate<GameObject>(prefabBullet);
                        goBullet.transform.rotation = bulletAnchorTrans.rotation;
                        bullet = goBullet.GetComponent<Bullet>();
                        goBullet.transform.parent = bulletAnchorTrans;
                        goBullet.transform.localPosition = new Vector3(0, 0, 0);
                        TimeShootNext = Time.time-generatedTime + ShootDelay;
                        mode = WeaponMode.shoot;
                        Shooting();
                    }
                    break;
            }
        }  
    }

    private void BeforeShootingTime()
    {
        if(mode == WeaponMode.readyShoot)
        {
            if(((int)(Time.time/0.08))%2 == 0)
                pistolSRend.enabled = false;
            else
                pistolSRend.enabled = true;
        }
    }

    protected override void Shooting()
    {
        bullet.dir = Direction2D;
        bullet.mode = Bullet.BulletMode.gOut;
        bullet.liveDone = TimeShootNext;
    }
}
