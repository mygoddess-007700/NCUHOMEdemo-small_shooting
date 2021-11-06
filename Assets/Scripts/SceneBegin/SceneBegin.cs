using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBegin : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Button btnA;
    public Button btnB;
    public Button btnC;
    public Animator animTop;
    public Animator animDown;
    public Animator animLOGO;
    [Header("Set Dynamically")]
    public GameObject LOGO;

    void Start() 
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        LOGO = transform.Find("LOGO").gameObject;
        LOGO.SetActive(false);  

        btnA.onClick.AddListener(LoadSceneA);
        btnB.onClick.AddListener(LoadSceneB);
        btnC.onClick.AddListener(LoadSceneC);
    }

    private void LoadSceneA()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex+1));
    }

    private void LoadSceneB()
    {
        StartCoroutine(LoadScene(3));
    }

    private void LoadSceneC()
    {
        StartCoroutine(LoadScene(3));
    }

    IEnumerator LoadScene(int index)
    {
        LOGO.SetActive(true);
        animTop.CrossFade("TopFadeIn", 0);
        animDown.CrossFade("DownFadeIn", 0);
        animLOGO.CrossFade("LOGOFadeIn", 0);

        yield return new WaitForSeconds(2f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation obj)
    {
        animTop.CrossFade("TopFadeOut", 0);
        animDown.CrossFade("DownFadeOut", 0);
        animLOGO.CrossFade("LOGOFadeOut", 0);
    }

}
