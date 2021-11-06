using UnityEngine;

public class Cover : MonoBehaviour
{
    [Header("Set In Inspector")]
    public InRoom heroInRoom;
    public GameObject prefabCloseEnemy;
    public float enemyFrequencyMin = 2f;
    public float enemyFrequencyMax = 4f;
    public float firstEnemyTimeMax = 3f;

    [Header("Set Dynamically")]
    public InRoom inRoom;
    public bool hasHero = false;

    private float timeEnemyNext = 0;
    
    void Start() 
    {
        inRoom = GetComponent<InRoom>();    
    }

    void Update() 
    {
        if(hasHero && timeEnemyNext == 0)
        {
            timeEnemyNext = Time.time + Random.Range(0, firstEnemyTimeMax);
        }
        if(hasHero)
        {
            if(Time.time > timeEnemyNext)
            {
                GameObject enemyGO = Instantiate<GameObject>(prefabCloseEnemy);
                enemyGO.transform.parent = transform;
                enemyGO.transform.localPosition = new Vector3(0, 0, 0);
                timeEnemyNext = Time.time + Random.Range(enemyFrequencyMin, enemyFrequencyMax);
            } 
        }

        hasHero = (inRoom.RoomNum == heroInRoom.RoomNum) ? true : false;
    }
}
