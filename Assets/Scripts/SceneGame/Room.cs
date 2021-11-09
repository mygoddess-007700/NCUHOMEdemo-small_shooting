using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Set In Inspector: Room")]
    public InRoom heroInRoom;
    public float enemyFrequencyMin = 2f;
    public float enemyFrequencyMax = 4f;
    public float enemyBeginTime = 5f;
    public GUIPanel gui;

    [Header("Set Dynamically: Room")]
    public InRoom inRoom;
    public bool hasHero = false;

    protected float timeEnemyNext;

    protected virtual void Start() 
    {
        inRoom = GetComponent<InRoom>();
    }
}
