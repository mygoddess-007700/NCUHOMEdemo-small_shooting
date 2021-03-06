using UnityEngine;

public interface IWeapon
{
    Vector2 Direction2D{get; set;}
    void Rotate();
    // float ShootDuration{get;}
    float ShootDelay{get;}
    float TimeShootNext{get; set;}
    Bullet WeaponBullet{get;}   
}
