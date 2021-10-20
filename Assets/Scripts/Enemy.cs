using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum eMode{idle, readyShoot, shoot, move};

    [Header("Set In Spector")]
    public int closeDamage = 1;
    public int maxHealth = 1;
    public float invincibleDuration = 0.5f;
    public bool hasWeapon;
    public int score = 100;

    [Header("Set Dynamically")]
    public Hero hero;
    public int health;
    public bool invincible = false;
    public eMode mode = eMode.idle;
    public InRoom heroInRoom;
    public InRoom inRoom;

    protected float invincibleDone = 0;
    protected Vector2 dir = Vector2.zero;

    protected Animator anim;
    protected Rigidbody rigid;
    protected SpriteRenderer sRend;
    protected float generatedTime = 0;

    protected virtual void Awake() 
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        sRend = GetComponent<SpriteRenderer>();

        GameObject heroGO = GameObject.Find("Hero");
        hero = heroGO.GetComponent<Hero>();
        heroInRoom = heroGO.GetComponent<InRoom>();
        inRoom = GetComponent<InRoom>();
    }

    protected virtual void Start() 
    {
        generatedTime = Time.time;    
    }

    protected virtual void Update() 
    {
        dir = hero.transform.position - transform.position;
        dir.Normalize();
        //确认是否受击，如果受击施加无敌
        if(invincible && Time.time-generatedTime > invincibleDone)
        {
            invincible = false;
            sRend.enabled = true;
        }

        if(invincible)
        {
            if(((int)(Time.time/0.08))%2 == 0)
                sRend.enabled = false;
            else
                sRend.enabled = true;
        }

        if(inRoom.RoomNum != heroInRoom.RoomNum)    
            Destroy(gameObject);
    }

    public Vector2 Direction2D
    {
        get{return dir;}
    }
}
