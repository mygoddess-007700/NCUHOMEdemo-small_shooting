using UnityEngine;

public class CloseEnemy : Enemy
{
    [Header("Set In Inspector: CloseEnemy")]
    public float speed = 1f;
    public int damage = 1;

    protected override void Update() 
    {
        base.Update();
        mode = eMode.move;

        rigid.velocity = dir * speed;
    }
}
