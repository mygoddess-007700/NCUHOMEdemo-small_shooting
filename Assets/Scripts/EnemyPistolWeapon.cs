using UnityEngine;

public class EnemyPistolWeapon : Weapon
{
    public enum EnemyPistolWeaponMode{idle, move, readyNext, shoot};

    [Header("Set In Inspector: EnemyPistolWeapon")]
    public float beforeShootingTime = 3f;

    [Header("Set Dynamically: EnemyPistolWeapon")]
    public EnemyPistolWeaponMode mode = EnemyPistolWeaponMode.idle;
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            if(Time.time < beforeShootingTime)
            {
                mode = EnemyPistolWeaponMode.move;
                MoveInBeforeShootingTime();
                return;
            }

            if(Input.GetMouseButton(0) && Time.time >= TimeShootNext)
            {
                mode = EnemyPistolWeaponMode.shoot;
                TimeShootNext = Time.time + shootDelay;
                Shooting();
            }

            //攻击结束
            if(Time.time >= TimeShootDone)
            {
                mode = EnemyPistolWeaponMode.readyNext;
            }
        }  
    }

    private void MoveInBeforeShootingTime()
    {

    }

    protected override void Shooting()
    {
        
    }

    protected override void DestroyBullet()
    {

    }
}
