using UnityEngine;

public class HeroMove : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Hero hero;
    public GUIPanel gui;

    [Header("Set Dynamically")]
    public Rigidbody heroRigid;

    void Start() 
    {
        heroRigid = hero.transform.GetComponent<Rigidbody>(); 
        heroRigid.velocity = Vector3.zero;  
        FollowCam.POI = hero.gameObject;    
    }

    void Update() 
    {
        // if(hero.score > 4000)
        // {
        //     heroRigid.velocity = Vector3.up;
        //     if(hero.transform.position.y > 38.5f)
        //     {
        //         heroRigid.velocity = Vector3.zero;
        //     } 
        //     return; 
        // }
        if(hero.score > 3000)
        {
            heroRigid.velocity = Vector3.up;
            if(hero.transform.position.y > 27.5f)
            {
                heroRigid.velocity = Vector3.zero;
            } 
            return; 
        }
        if(hero.score > 2000)
        {
            heroRigid.velocity = Vector3.up;
            if(hero.transform.position.y > 14.5f)
            {
                heroRigid.velocity = Vector3.zero;
            } 
            return; 

        }
        if(hero.score > 1000)
        {
            heroRigid.velocity = Vector3.right;
            if(hero.transform.position.x > 24f)
            {
                heroRigid.velocity = Vector3.zero;
            } 
            return; 
        }  
    }
}
