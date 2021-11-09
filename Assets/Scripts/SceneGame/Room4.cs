// using UnityEngine;

// public class Room4 : Room
// {
//     [Header("Set In Inspector: Room1")]
//     public Vector3 grapplerPos = Vector3.zero;

//     protected override void Start() 
//     {
//         base.Start();
        
//     }

//     void Update() 
//     {
//         if(!hasHero)
//         {
//             timeEnemyNext = timeEnemyNext + Time.deltaTime;
//         }
//         if(hasHero)
//         {
//             gui.roomNum = 2;
//             if(Time.time > timeEnemyNext)
//             {
//                 GenerateEnemy();
//                 timeEnemyNext +=  Random.Range(enemyFrequencyMin, enemyFrequencyMax);
//             }
//         }
//         hasHero = (inRoom.RoomNum == heroInRoom.RoomNum) ? true : false;
//     }

//     void GenerateEnemy()
//     {
//         int indexPos = Random.Range(0, enemyPos.Length);
//         GameObject enemyGO;
//         if(indexPos==2 || indexPos==3)
//         {
//             enemyGO = Instantiate<GameObject>(prefabEnemy[0]);
//         }
//         else
//         {
//             if(Random.Range(0, 1.0f) < enemyProbability[0])
//                 enemyGO = Instantiate<GameObject>(prefabEnemy[0]);
//             else
//                 enemyGO = Instantiate<GameObject>(prefabEnemy[1]); 
//         }
//         enemyGO.transform.parent = this.transform;
//         enemyGO.transform.localPosition = enemyPos[indexPos];
//     }
// }
