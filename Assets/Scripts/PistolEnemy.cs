using UnityEngine;

public class PistolEnemy : Enemy
{
    [Header("Set In Inspector: CloseEnemy")]
    public float speed = 0.5f;
    public float moveDuration = 1.5f;
    public float readyShootDuration = 0.5f;

    private Sprite[] pistolSprite;
    private SpriteRenderer pistolSpriteRender;
    private Transform rotationCenter;
    private Transform pistol;
    private Transform bullet_Anchor;
    private Vector3 rightRotationCenterPosition;
    private Vector3 leftRotationCenterPosition;
    private Vector3 rightPistolPosition;
    private Vector3 leftPistolPosition;
    private Vector3 rightBulletPosition;
    private Vector3 leftBulletPosition;

    protected override void Start() 
    {
        base.Start();
        pistolSprite = new Sprite[2];
        rotationCenter = transform.Find("RotationCenter");
        pistol = rotationCenter.Find("Pistol");
        pistolSpriteRender = pistol.GetComponent<SpriteRenderer>();

        pistolSprite[0] = Resources.Load<Sprite>("RightPistol");
        pistolSprite[1] = Resources.Load<Sprite>("LeftPistol");

        bullet_Anchor = pistol.Find("Bullet_Anchor");

        rightRotationCenterPosition = new Vector3(0.25f, 0, 0);
        leftRotationCenterPosition = new Vector3(-0.35f, 0, 0);
        rightPistolPosition = new Vector3(0.25f, 0, 0);
        leftPistolPosition = new Vector3(-0.35f, 0, 0);
        rightBulletPosition = new Vector3(-0.05f, 0.15f, 0);
        leftBulletPosition = new Vector3(0.05f, 0.1f, 0);
    }

    protected override void Update() 
    {
        base.Update();
  
        if(Time.time-generatedTime < moveDuration)
        {
            rigid.velocity =  speed * dir; 
            mode = eMode.move;
        }
        else if(Time.time-generatedTime < moveDuration+readyShootDuration)
        {
            rigid.velocity = Vector3.zero;
            mode = eMode.readyShoot;
        }
        else
        {
            mode = eMode.shoot;
        }

        if(dir.x > 0)
        {
            pistolSpriteRender.sprite = pistolSprite[0];
            rotationCenter.transform.localPosition = rightRotationCenterPosition;
            pistol.transform.localPosition = rightPistolPosition;
            bullet_Anchor.transform.localPosition = rightBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else if(dir.x < 0)
        {
            pistolSpriteRender.sprite = pistolSprite[1];
            rotationCenter.transform.localPosition = leftRotationCenterPosition;
            pistol.transform.localPosition = leftPistolPosition;
            bullet_Anchor.transform.localPosition = leftBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else
        {
            pistolSpriteRender.sprite = pistolSprite[0];
            rotationCenter.transform.localPosition = rightRotationCenterPosition;
            pistol.transform.localPosition = rightPistolPosition;
            bullet_Anchor.transform.localPosition = rightBulletPosition;
            if(dir.y > 0)
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if(dir.y < 0)
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, -90);
            else
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, 0); 
        }
    }

    void OnCollisionEnter(Collision coll) 
    {
        invincibleDone = Time.time-generatedTime + invincibleDuration;
        if(coll.gameObject.tag == "HeroBullet")
        {
            invincible = true;
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            health -= bullet.Damage;
            if(health <= 0)
            {
                hero.score += score;
                Destroy(gameObject);
            }
            Destroy(coll.gameObject);
        }
    }
}
