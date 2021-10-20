using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Hero hero;
    public Material groundMat;

    [Header("Set Dynamically")]
    public Rigidbody heroRigid;

    void Start() 
    {
        heroRigid = hero.transform.GetComponent<Rigidbody>(); 
        heroRigid.velocity = Vector3.zero;   
    }

    void Update() 
    {
        if(hero.score >= 2000)
        {
            heroRigid.velocity = Vector3.right;
            FolowCam.POI = hero.gameObject;
        }
        if(hero.transform.position.x > 34f)
        {
            heroRigid.velocity = Vector3.zero;
            FolowCam.POI = null;
            GUIPanel.game = 1;
            groundMat.color = Color.grey;
        }
        if(hero.transform.position.x > 39f) 
        {
            GUIPanel.game = 1;
            groundMat.color = Color.grey;
        }   
    }
}
