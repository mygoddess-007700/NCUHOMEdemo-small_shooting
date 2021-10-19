using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    int Damage{get;}
    Vector2 Direction2D{get; set;}
    void Rotate();
    float ShootDuration{get;}
    float ShootDelay{get;}
    float TimeShootDone{get; set;}
    float TimeShootNext{get; set;}
    Bullet WeaponBullet{get;}   
}
