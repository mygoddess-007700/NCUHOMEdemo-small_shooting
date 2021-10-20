using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;

public class DoorWall : MonoBehaviour
{
    [Header("Set In Inspector")]
    public InRoom heroInRoom;
    public GameObject prefabCloseEnemy;
    public GameObject prefabPistolEnemy;
    public float enemyFrequencyMin = 2f;
    public float enemyFrequencyMax = 4f;
    public float CloseEnemyFrequency = 0.8f;
    public float firstEnemyTimeMax = 5f;

    [Header("Set Dynamically")]
    public InRoom inRoom;
    public List<Transform> door;
    public bool hasHero = false;

    private List<float> timeEnemyNext;
    
    void Start() 
    {
        door = new List<Transform>();
        timeEnemyNext = new List<float>();
        for(int i=0; i<20; i++)
        {
            Transform trans = transform.Find("Door"+i); 
            if(trans == null) 
                break;
            door.Add(trans);
            timeEnemyNext.Add(Random.Range(0, firstEnemyTimeMax));
        }

        inRoom = GetComponent<InRoom>();
    }

    void Update() 
    {
        if(hasHero && timeEnemyNext[0] <= firstEnemyTimeMax)
        {
            for(int i=0; i<timeEnemyNext.Count; i++)
            {
                timeEnemyNext[i] += Time.time;
            }
        }
        if(hasHero)
        {
            for(int i=0; i<door.Count; i++)
            {
                if(Time.time > timeEnemyNext[i])
                {
                    GameObject enemyGO;
                    if(Random.Range(0, 1.0f)<CloseEnemyFrequency)
                    {
                        enemyGO = Instantiate<GameObject>(prefabCloseEnemy);
                    }
                    else
                    {
                        enemyGO = Instantiate<GameObject>(prefabPistolEnemy);
                    }
                    enemyGO.transform.parent = door[i];
                    enemyGO.transform.localPosition = new Vector3(0, 0, 0);
                    timeEnemyNext[i] = Time.time + Random.Range(enemyFrequencyMin, enemyFrequencyMax);
                }
            }
        }

        hasHero = (inRoom.RoomNum == heroInRoom.RoomNum) ? true : false;
    }
}
