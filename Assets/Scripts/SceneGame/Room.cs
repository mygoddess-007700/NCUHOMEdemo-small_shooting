using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Set In Inspector: Room")]
    public InRoom heroInRoom;
    public GameObject prefabCloseEnemy;
    public GameObject prefabPistolEnemy;
    public float enemyFrequencyMin = 2f;
    public float enemyFrequencyMax = 4f;
    public float CloseEnemyFrequency = 0.8f;
    public float firstEnemyTimeMax = 3f;

    [Header("Set Dynamically: Room")]
    public InRoom inRoom;
    public bool hasHero = false;

    protected List<float> timeEnemyNext;

    protected virtual void Start() 
    {
        inRoom = GetComponent<InRoom>();
    }
}
