using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : Room
{
    [Header("Set In Inspector: Room1")]
    public Vector3[] enemyPos = new Vector3[]
    {
        new Vector3(-4.5f, 3f, 0),
        new Vector3(4.5f, -3.5f, 0),

        new Vector3(7f, 4.5f, 0),
        new Vector3(-7.5f, -4.5f, 0)
    };
    public GameObject[] prefabEnemy = new GameObject[2];
    public float[] enemyProbability = new float[2];

    protected override void Start() 
    {
        base.Start();
        timeEnemyNext = Time.time + enemyBeginTime;
    }

    void Update() 
    {
        if(!hasHero)
        {
            timeEnemyNext = timeEnemyNext + Time.deltaTime;
        }
        if(hasHero)
        {
            gui.roomNum = 2;
            if(Time.time > timeEnemyNext)
            {
                GenerateEnemy();
                timeEnemyNext +=  Random.Range(enemyFrequencyMin, enemyFrequencyMax);
            }
        }
        hasHero = (inRoom.RoomNum == heroInRoom.RoomNum) ? true : false;
    }

    void GenerateEnemy()
    {
        int indexPos = Random.Range(0, enemyPos.Length);
        GameObject enemyGO;
        if(indexPos==2 || indexPos==3)
        {
            enemyGO = Instantiate<GameObject>(prefabEnemy[0]);
        }
        else
        {
            if(Random.Range(0, 1.0f) < enemyProbability[0])
                enemyGO = Instantiate<GameObject>(prefabEnemy[0]);
            else
                enemyGO = Instantiate<GameObject>(prefabEnemy[1]); 
        }
        enemyGO.transform.parent = this.transform;
        enemyGO.transform.localPosition = enemyPos[indexPos];
    }
}
