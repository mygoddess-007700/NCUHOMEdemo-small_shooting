using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SceneEnd : MonoBehaviour
{
    [Header("Set In Inspector")]
    public TextAsset EndFile;
    public Image img;
    public GameObject GameEndPanel;
    public GameObject EndPanel;
    public TMP_Text EndText;
    public Animator keyAnim;
    public Animator fadeAnim;
    public Sprite end1;
    public float loadingTime = 1f;

    [Header("Set Dynamically")]
    public float textSpeed = 0.1f;

    private List<string> textList = new List<string>();
    private int index = 0;
    private bool textFinished = true;
    private bool beginFade = false;
    private float loadingTimeDone = 0;

    void Awake() 
    {
        GameEndPanel.SetActive(false);    
    }

    void Start() 
    {
        GetTextFromFile(EndFile);
        EndText.text = "";
        loadingTimeDone = Time.time + loadingTime;
    }

    void Update() 
    {
        if(Time.time < loadingTimeDone)
        {
            return;
        }

        if(index == textList.Count)
        {
            if(Input.anyKeyDown)
            {
                loadingTimeDone = Time.time + loadingTime;
                fadeAnim.CrossFade("FadeIn", 0);
                StartCoroutine("LoadFadeOut");
            }
            else
            {
                return;
            }
        }
        if(Input.anyKeyDown && index < textList.Count)
        {
            if(textFinished == true)
            {
                if(beginFade)
                {
                    loadingTimeDone = Time.time + loadingTime;
                    fadeAnim.CrossFade("FadeIn", 0);
                    Invoke("SwitchImg", 2f);
                }
                else
                {
                    StartCoroutine(ShowDialogue(index+2));
                }

            }
            else if(textFinished == false)
            {
                textSpeed = 0.01f;
            }
        }
    }

    IEnumerator LoadFadeOut()
    {
        yield return new WaitForSeconds(2f);
        fadeAnim.CrossFade("FadeOut", 0);
        GameEndPanel.SetActive(true);
        img.gameObject.SetActive(false);
        EndPanel.SetActive(false);
    }

    void SwitchImg()
    {
        EndText.text = "";
        int i = index/2 + 1;
        switch(i)
        {
            case 2:
                img.sprite = end1;
                break;
        }
        fadeAnim.CrossFade("FadeOut", 0);
        beginFade = false;
    }

    IEnumerator ShowDialogue(int last)
    {
        textFinished = false;
        EndText.text = "";

        while(index < last)
        {
            for(int i=0; i<textList[index].Length; ++i)
            {
                EndText.text += textList[index][i];

                yield return new WaitForSeconds(textSpeed);
            }
            EndText.text += "\n";
            index++;
        }
        beginFade = true;
        textFinished = true;
        textSpeed = 0.1f;
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        string [] lineData = file.text.Split('\n');

        foreach(string line in lineData)
        {
            textList.Add(line);
        }
    }
}
