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
    public Animator animator;

    void Start() 
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(eventObj); 
        StartCoroutine(LoadScene(1));
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
