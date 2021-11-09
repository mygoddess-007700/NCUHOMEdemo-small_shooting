using System.Collections;
using UnityEngine;

public class HeroPistolWeapon : Weapon
{
    [Header("Set In Inspector: HeroPistolWeapon")]
    public Hero hero;
    public float reloadTime = 2f;

    [Header("Set Dynamically: HeroPistolWeapon")]
    bool ifReload = false;

    void Start() 
    {
        Transform trans = transform.Find("Pistol");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
    }

    void Update()
    {
        Direction2D = hero.dir;
<<<<<<< HEAD:Assets/Scripts/SceneGame/HeroPistolWeapon.cs
        print(Input.mousePosition.x);
        if(Input.mousePosition.x > 1760)
=======
        if(Input.mousePosition.x > Screen.width * 0.8)
        {
            return;
        }
        if(hero.bulletNum == 0 && !ifReload)
>>>>>>> div:Assets/Scripts/HeroPistolWeapon.cs
        {
            ifReload = true;
            StartCoroutine("Reload");
            return;
        }
        if(Input.GetMouseButton(0) && Time.time >= TimeShootNext && hero.bulletNum > 0)
        {
            GameObject goBullet = Instantiate<GameObject>(prefabBullet);
            goBullet.transform.rotation = bulletAnchorTrans.rotation;
            bullet = goBullet.GetComponent<Bullet>();
            goBullet.transform.parent = bulletAnchorTrans;
            goBullet.transform.localPosition = new Vector3(0, 0, 0);
            TimeShootNext = Time.time + ShootDelay;
            // TimeShootDone = Time.time + ShootDuration;

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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        hero.bulletNum = hero.maxBulletNum;
        ifReload = false;
    }
}
