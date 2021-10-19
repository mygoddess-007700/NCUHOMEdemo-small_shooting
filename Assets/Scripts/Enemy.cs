using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum eMode{idle, shoot, move, isShooted};

    [Header("Set In Spector")]
    public int closeDamage = 1;
    public int maxHealth = 1;
    public bool hasWeapon;

    [Header("Set Dynamically")]
    public int health;
    public eMode mode = eMode.idle;
    public InRoom heroInRoom;
}
