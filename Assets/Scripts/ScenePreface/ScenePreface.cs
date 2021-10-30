using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreface : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float countTime = 0f;

    private GameObject fadeInGO;
    void Start() 
    {
        fadeInGO = transform.Find("FadeIn").gameObject;
        fadeInGO.SetActive(false);
    }
    void Update()
    {
        CountTime();
    }

    void CountTime()
    {
        countTime += Time.deltaTime;
        if(countTime > 2.5f)
        {
            fadeInGO.SetActive(true);
        }
        if(countTime > 3.5f)
        {
            SceneManager.LoadScene("SceneBegin");
        }
    }
}
