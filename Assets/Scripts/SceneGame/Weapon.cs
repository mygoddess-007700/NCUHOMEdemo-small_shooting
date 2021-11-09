using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public enum WeaponMode{idle, readyShoot, shoot};
    [Header("Set In Inspector: Weapon")]
    public float shootDelay = 0.4f; //目前shootDelay要大于shootDuration
    // public float shootDuration = 0.25f;
    public GameObject prefabBullet;

    private Vector2 dir = Vector2.zero;
    private float timeShootNext = 0;

    protected Transform bulletAnchorTrans;
    protected Bullet bullet;

    protected abstract void Shooting();

    public Vector2 Direction2D
    {
        get{return dir;}
        set{dir = value;}
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
    }

    // public float ShootDuration
    // {
    //     get{return shootDuration;}
    // }

    public float ShootDelay
    {
        get{return shootDelay;}
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
