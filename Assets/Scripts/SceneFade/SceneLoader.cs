using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject eventObj;
    public Button btnA;
    public Button btnB;
    public Animator animator;

    void Start() 
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(eventObj);    

        btnA.onClick.AddListener(LoadSceneA);
        btnB.onClick.AddListener(LoadSceneB);
    }

    private void LoadSceneA()
    {
        StartCoroutine(LoadScene(1));
    }

    private void LoadSceneB()
    {
        StartCoroutine(LoadScene(2));
    }

    IEnumerator LoadScene(int index)
    {
        animator.CrossFade("FadeIn", 0);

        yield return new WaitForSeconds(1f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation obj)
    {
        animator.CrossFade("FadeOut", 0);
    }

}
