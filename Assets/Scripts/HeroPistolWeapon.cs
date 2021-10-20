using UnityEngine;

public class HeroPistolWeapon : Weapon
{
    public enum HeroPistolWeaponMode{idle, shoot};

    [Header("Set In Inspector: HeroPistolWeapon")]
    public Hero hero;

    [Header("Set Dynamically: HeroPistolWeapon")]
    public HeroPistolWeaponMode mode = HeroPistolWeaponMode.idle;

    protected override void Start() 
    {
        base.Start();
        Transform trans = transform.Find("Pistol");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
    }

    void Update()
    {
        Direction2D = hero.dir;
        if(this.gameObject.activeSelf)
        {
            if(Input.GetMouseButton(0) && Time.time-generatedTime >= TimeShootNext && hero.bulletNum > 0)
            {
                GameObject goBullet = Instantiate<GameObject>(prefabBullet);
                goBullet.transform.rotation = bulletAnchorTrans.rotation;
                bullet = goBullet.GetComponent<Bullet>();
                goBullet.transform.parent = bulletAnchorTrans;
                goBullet.transform.localPosition = new Vector3(0, 0, 0);
                mode = HeroPistolWeaponMode.shoot;
                TimeShootNext = Time.time-generatedTime + ShootDelay;
                TimeShootDone = Time.time-generatedTime + ShootDuration;
                Shooting();
            }
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
