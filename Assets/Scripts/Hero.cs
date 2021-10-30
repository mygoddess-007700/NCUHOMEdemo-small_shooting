using UnityEngine;

public class Hero : MonoBehaviour
{
    public enum eMode{idle, shoot, move};

    [Header("Set In Spector")]
    public float speed = 0.5f;
    public int maxHealth = 3;
    public float invincibleDuration = 0.5f;
    public int maxBulletNum = 5;
    public Material groundMat;

    public int score = 0;


    [Header("Set Dynamically")]
    public bool shooting = false;
    public int facing = 1; //1表示人物朝向右，-1表示人物朝向左
    public int moves = 0; //移动的次数
    public eMode mode = eMode.idle;
    public bool invincible = false;
    public bool hasGrappler = false;
    public int bulletNum;
    public Vector2 dir = Vector2.zero; //鼠标位置单位化

    [SerializeField]
    private int _health;

    public int Health
    {
        get{return _health;}
        set
        {
            _health = value;
            // if(_health <= 0)
            // heroDie();
        }
    }

    private float invincibleDone = 0;
    private Sprite[] pistolSprite;
    private SpriteRenderer sRend;
    private SpriteRenderer pistolSpriteRender;
    private Animator anim;
    private InRoom inRoom;
    private Transform rotationCenter;
    private Transform heroPistol;
    private Transform bullet_Anchor;
    private Vector3 rightRotationCenterPosition;
    private Vector3 leftRotationCenterPosition;
    private Vector3 rightPistolPosition;
    private Vector3 leftPistolPosition;
    private Vector3 rightBulletPosition;
    private Vector3 leftBulletPosition;

    private Vector3[] directions = new Vector3[]
    {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };

    void Awake() 
    {
        groundMat.color = Color.yellow;
        anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        inRoom = GetComponent<InRoom>();
        Health = maxHealth;
    }

    void Start() 
    {
        pistolSprite = new Sprite[2];
        rotationCenter = transform.Find("RotationCenter");
        heroPistol = rotationCenter.Find("Pistol");
        pistolSpriteRender = heroPistol.GetComponent<SpriteRenderer>();

        pistolSprite[0] = Resources.Load<Sprite>("RightPistol");
        pistolSprite[1] = Resources.Load<Sprite>("LeftPistol");

        bullet_Anchor = heroPistol.Find("Bullet_Anchor");

        rightRotationCenterPosition = new Vector3(0.25f, 0, 0);
        leftRotationCenterPosition = new Vector3(-0.35f, 0, 0);
        rightPistolPosition = new Vector3(0.25f, 0, 0);
        leftPistolPosition = new Vector3(-0.35f, 0, 0);
        rightBulletPosition = new Vector3(-0.05f, 0.15f, 0);
        leftBulletPosition = new Vector3(0.05f, 0.1f, 0);
    }

    void Update() 
    {
        if(Health == 0)
        {
            groundMat.color = Color.green;
            GUIPanel.game = -1;
        }
        //确认是否受击，如果受击施加无敌
        if(invincible && Time.time > invincibleDone)
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

        //根据鼠标位置更改人的左右与枪的左右与枪的旋转
        //计算鼠标位置(单位化)
        dir = mousePos2D() - (Vector2)transform.position;
        dir.Normalize();
        if(dir.x > 0)
        {
            facing = 1;
            anim.CrossFade("HeroRight", 0);

            pistolSpriteRender.sprite = pistolSprite[0];

            rotationCenter.transform.localPosition = rightRotationCenterPosition;
            heroPistol.transform.localPosition = rightPistolPosition;
            bullet_Anchor.transform.localPosition = rightBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else
        {
            facing = -1;
            anim.CrossFade("HeroLeft", 0);

            pistolSpriteRender.sprite = pistolSprite[1];
            rotationCenter.transform.localPosition = leftRotationCenterPosition;
            heroPistol.transform.localPosition = leftPistolPosition;
            bullet_Anchor.transform.localPosition = leftBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
    }

    void OnTriggerEnter(Collider colld) 
    {


        if(colld.gameObject.tag == "Enemy")
        {
            if(invincible) 
            {
                Destroy(colld.gameObject);
                return;
            }

            invincibleDone = Time.time + invincibleDuration;
            invincible = true;
            Enemy enemy = colld.gameObject.GetComponent<Enemy>();
            Health -= enemy.closeDamage;
            Destroy(colld.gameObject);
        }
        else if(colld.gameObject.tag == "EnemyBullet")
        {
            if(invincible) 
            {
                Destroy(colld.gameObject);
                return;
            }

            invincibleDone = Time.time + invincibleDuration;
            invincible = true;
            Bullet bullet = colld.gameObject.GetComponent<Bullet>();
            Health -= bullet.Damage;
            Destroy(colld.gameObject);
        }
        else
        {
            return;
        } 
    }

    public Vector2 mousePos2D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return (Vector2)mousePos;
    }
}
