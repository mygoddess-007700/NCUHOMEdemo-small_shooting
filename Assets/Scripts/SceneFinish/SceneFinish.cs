using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SceneFinish : MonoBehaviour
{
    [Header("Set In Inspector")]
    public TextAsset FinishFile;
    public Image img;
    public GameObject GameFinishPanel;
    public GameObject FinishPanel;
    public TMP_Text FinishText;
    public Animator keyAnim;
    public Animator fadeAnim;
    public Sprite Finish1;
    public Sprite Finish2;
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
        GameFinishPanel.SetActive(false);    
    }

    void Start() 
    {
        GetTextFromFile(FinishFile);
        FinishText.text = "";
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
        GameFinishPanel.SetActive(true);
        img.gameObject.SetActive(false);
        FinishPanel.SetActive(false);
    }

    void SwitchImg() //有Bug，不能case 3
    {
        FinishText.text = "";
        int i = index/2 + 1;
        switch(i)
        {
            case 2:
                img.sprite = Finish1;
                break;
            // case 3:
            //     print("fxd");
            //     img.sprite = Finish2;
            //     break;
        }
        fadeAnim.CrossFade("FadeOut", 0);
        beginFade = false;
    }

    IEnumerator ShowDialogue(int last)
    {
        textFinished = false;
        FinishText.text = "";

        while(index < last)
        {
            for(int i=0; i<textList[index].Length; ++i)
            {
                FinishText.text += textList[index][i];

                yield return new WaitForSeconds(textSpeed);
            }
            FinishText.text += "\n";
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
