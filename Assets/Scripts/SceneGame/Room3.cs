using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3 : Room
{
    [Header("Set In Inspector: Room1")]
    public Vector3[] enemyPos = new Vector3[]
    {
        new Vector3(0, 4f, 0),

        new Vector3(7.5f, 2.5f, 0),
        new Vector3(7.5f, 0, 0),
        new Vector3(7.5f, -2.5f, 0),
        new Vector3(-7.5f, 2.5f, 0),
        new Vector3(-7.5f, 0, 0),
        new Vector3(-7.5f, -2.5f, 0),
    };
    public GameObject[] prefabEnemy = new GameObject[2];
    public float rocketEnemyBeginTime = 1f;
    public float rockerEnemyGenerateTime = 5f;
    public Hero hero;

    private float rocketEnemyNext = 0;

    protected override void Start() 
    {
        base.Start();
        timeEnemyNext = Time.time + enemyBeginTime;
        rocketEnemyNext = Time.time + rocketEnemyBeginTime;
    }

    void Update() 
    {
        if(!hasHero)
        {
            rocketEnemyNext = rocketEnemyNext + Time.deltaTime;
            timeEnemyNext = timeEnemyNext + Time.deltaTime;
        }
        if(hasHero)
        {
            gui.roomNum = 3;
            if(Time.time > rocketEnemyNext)
            {
                if(hero.score < 3000)
                {
                    GameObject enemyGO = Instantiate<GameObject>(prefabEnemy[1]);
                    enemyGO.transform.parent = this.transform;
                    enemyGO.transform.localPosition = enemyPos[0];

                    rocketEnemyNext +=  rockerEnemyGenerateTime;
                }

            }
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
        int indexPos = Random.Range(1, enemyPos.Length);
        GameObject enemyGO = Instantiate<GameObject>(prefabEnemy[0]);
        enemyGO.transform.parent = this.transform;
        enemyGO.transform.localPosition = enemyPos[indexPos];
    }
}
