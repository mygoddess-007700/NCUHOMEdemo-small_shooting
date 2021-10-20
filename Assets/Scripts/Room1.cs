using System.Collections.Generic;
using UnityEngine;

public class Room1 : Room
{
    [Header("Set In Inspector: Room")]
    Vector3[] enemyPoints = new Vector3[]
    {
        new Vector3(7.5f, 4.5f, 0),
        new Vector3(7.5f, -4.5f, 0),
        new Vector3(-7.5f, -4.5f, 0)
    };

    protected override void Start() 
    {
        base.Start();
        timeEnemyNext = new List<float>();
        for(int i=0; i<enemyPoints.Length; i++)
        {
            timeEnemyNext.Add(Random.Range(0, firstEnemyTimeMax));
        }
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
            for(int i=0; i<enemyPoints.Length; i++)
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
                    enemyGO.transform.parent = this.transform;
                    enemyGO.transform.localPosition = enemyPoints[i];
                    timeEnemyNext[i] = Time.time + Random.Range(enemyFrequencyMin, enemyFrequencyMax);
                }
            }
        }

        hasHero = (inRoom.RoomNum == heroInRoom.RoomNum) ? true : false;
    }
}
