using UnityEngine;

public class HeroPistolWeapon : Weapon
{
    public enum HeroPistolWeaponMode{idle, shoot};

    [Header("Set In Inspector: HeroPistolWeapon")]
    public Hero hero;

    [Header("Set Dynamically: HeroPistolWeapon")]
    public HeroPistolWeaponMode mode = HeroPistolWeaponMode.idle;

    void Start() 
    {
        Transform trans = transform.Find("Pistol");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
    }

    void LateUpdate()
    {
        Direction2D = hero.dir;
        if(Input.mousePosition.x > 1760)
        {
            return;
        }
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
    }

    protected override void Shooting()
    {
        hero.bulletNum--;
        bullet.dir = Direction2D;
        bullet.mode = Bullet.BulletMode.gOut;
        bullet.liveDone = TimeShootNext;
    }
}
