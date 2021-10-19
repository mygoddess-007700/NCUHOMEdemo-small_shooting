using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [Header("Set In Inspector: Weapon")]
    public int damage = 1;
    public float shootDuration = 0.25f;
    public float shootDelay = 1f; //目前shootDelay要大于shootDuration
    public GameObject prefabBullet;
    private Vector2 dir = Vector2.zero;
    private float timeShootDone = 0;
    private float timeShootNext = 0;


    protected Transform bulletAnchorTrans;
    protected Bullet bullet;

    protected virtual void Start() 
    {
        Transform trans = transform.Find("HeroPistol");
        bulletAnchorTrans = trans.Find("Bullet_Anchor"); 
    }

    protected abstract void Shooting();
    protected abstract void DestroyBullet();

    public int Damage
    {
        get{return damage;}
    }

    public Vector2 Direction2D
    {
        get{return dir;}
        set{dir = value;}
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
    }

    public float ShootDuration
    {
        get{return shootDuration;}
    }

    public float ShootDelay
    {
        get{return shootDelay;}
    }

    public float TimeShootDone
    {
        get{return timeShootDone;}
        set{timeShootDone = value;}
    }

    public float TimeShootNext
    {
        get{return timeShootNext;}
        set{timeShootNext = value;}
    }

    public Bullet WeaponBullet
    {
        get{return bullet;}
    }
}
