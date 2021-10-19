using UnityEngine;

public class HeroPistolWeapon : Weapon
{
    public enum HeroPistolWeaponMode{idle, shoot, destroy};

    [Header("Set In Inspector: HeroPistolWeapon")]
    public Hero hero;

    [Header("Set Dynamically: HeroPistolWeapon")]
    public HeroPistolWeaponMode mode = HeroPistolWeaponMode.idle;

    protected override void Start() 
    {
        base.Start();
    }

    void Update()
    {
        switch(mode)
        {
            case HeroPistolWeaponMode.destroy:
                if(bullet != null)
                    DestroyBullet();
                break;
        }

        Direction2D = hero.dir;
        if(this.gameObject.activeSelf)
        {
            if(Input.GetMouseButton(0) && Time.time >= TimeShootNext && hero.bulletNum > 0)
            {
                GameObject goBullet = Instantiate<GameObject>(prefabBullet);
                goBullet.transform.rotation = bulletAnchorTrans.rotation;
                bullet = goBullet.GetComponent<Bullet>();
                goBullet.transform.parent = bulletAnchorTrans;
                goBullet.transform.localPosition = new Vector3(0, 0, 0);
                mode = HeroPistolWeaponMode.shoot;
                TimeShootNext = Time.time + ShootDelay;
                TimeShootDone = Time.time + ShootDuration;
                Shooting();
            }

            if(Time.time >= TimeShootDone && mode == HeroPistolWeaponMode.shoot)
            {
                mode = HeroPistolWeaponMode.destroy;
            }
        }  
    }

    protected override void Shooting()
    {
        hero.bulletNum--;
        bullet.dir = Direction2D;
        bullet.mode = Bullet.BulletMode.gOut;
    }

    protected override void DestroyBullet()
    {
        Destroy(bullet.gameObject);
        mode = HeroPistolWeaponMode.idle;
    }
}
