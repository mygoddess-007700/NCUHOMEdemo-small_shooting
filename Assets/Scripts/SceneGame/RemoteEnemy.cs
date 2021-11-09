using UnityEngine;

public class RemoteEnemy : Enemy
{
    [Header("Set In Inspector: RemoteEnemy")]
    public float speed = 0.5f;
    public float moveDuration = 1.5f;
    public float readyShootDuration = 0.5f;
    public Sprite[] weaponSprite = new Sprite[2]; //先右后左
    public Transform rotationCenter;
    public Transform weapon;
    public Transform bullet_Anchor;

    private SpriteRenderer weaponSpriteRender;
    private Vector3 rightRotationCenterPosition;
    private Vector3 leftRotationCenterPosition;
    private Vector3 rightWeaponPosition;
    private Vector3 leftWeaponPosition;
    private Vector3 rightBulletPosition;
    private Vector3 leftBulletPosition;

    protected override void Start() 
    {
        base.Start();
        rotationCenter = transform.Find("RotationCenter");
        weaponSpriteRender = weapon.GetComponent<SpriteRenderer>();

        rightRotationCenterPosition = new Vector3(0.25f, 0, 0);
        leftRotationCenterPosition = new Vector3(-0.35f, 0, 0);
        rightWeaponPosition = new Vector3(0.25f, 0, 0);
        leftWeaponPosition = new Vector3(-0.35f, 0, 0);
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
            weaponSpriteRender.sprite = weaponSprite[0];
            rotationCenter.transform.localPosition = rightRotationCenterPosition;
            weapon.transform.localPosition = rightWeaponPosition;
            bullet_Anchor.transform.localPosition = rightBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else if(dir.x < 0)
        {
            weaponSpriteRender.sprite = weaponSprite[1];
            rotationCenter.transform.localPosition = leftRotationCenterPosition;
            weapon.transform.localPosition = leftWeaponPosition;
            bullet_Anchor.transform.localPosition = leftBulletPosition;
            rotationCenter.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else
        {
            weaponSpriteRender.sprite = weaponSprite[0];
            rotationCenter.transform.localPosition = rightRotationCenterPosition;
            weapon.transform.localPosition = rightWeaponPosition;
            bullet_Anchor.transform.localPosition = rightBulletPosition;
            if(dir.y > 0)
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if(dir.y < 0)
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, -90);
            else
                rotationCenter.transform.rotation = Quaternion.Euler(0, 0, 0); 
        }
    }
}
