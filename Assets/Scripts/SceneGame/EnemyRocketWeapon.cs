using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketWeapon : Weapon
{
    [Header("Set Dynamically")]
    public WeaponMode mode = WeaponMode.idle;

    private bool firstFire = true;
    private SpriteRenderer rocketSRend;
    private Enemy enemy;
    private float generatedTime = 0;

    public bool FirstFire
    {
        get{return firstFire;}
        set{firstFire = value;}
    }

    void Start() 
    {
        generatedTime = Time.time;
        Transform trans = transform.Find("Rocket");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
        rocketSRend = trans.GetComponent<SpriteRenderer>();
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
                    rocketSRend.enabled = true;
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
                rocketSRend.enabled = false;
            else
                rocketSRend.enabled = true;
        }
    }

    protected override void Shooting()
    {
        bullet.dir = Direction2D;
        bullet.mode = Bullet.BulletMode.gOut;
        bullet.liveDone = TimeShootNext;
    }
}
