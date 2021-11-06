using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SceneDialogue : MonoBehaviour
{
    [Header("Set In Inspector")]
    public TextAsset dialogueFile;
    public Image img;
    public TMP_Text dialogueText;
    public Animator keyAnim;
    public Animator FadeAnim;
    public Sprite dialogue1;
    public Sprite dialogue2;
    public Sprite dialogue3;
    public float loadingTime = 1f;

    [Header("Set Dynamically")]
    public float textSpeed = 0.1f;

    private List<string> textList = new List<string>();
    private int index = 0;
    private bool textFinished = true;
    private bool beginFade = false;
    private float loadingTimeDone = 0;

    void Start() 
    {
        GetTextFromFile(dialogueFile);
        dialogueText.text = "";
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
                FadeAnim.CrossFade("FadeIn", 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
                    FadeAnim.CrossFade("FadeIn", 0);
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

    void SwitchImg()
    {
        dialogueText.text = "";
        int i = index/2 + 1;
        switch(i)
        {
            case 2:
                img.sprite = dialogue2;
                break;
            case 3:
                img.sprite = dialogue3;
                break;
        }
        FadeAnim.CrossFade("FadeOut", 0);
        beginFade = false;
    }

    IEnumerator ShowDialogue(int last)
    {
        textFinished = false;
        dialogueText.text = "";

        while(index < last)
        {
            for(int i=0; i<textList[index].Length; ++i)
            {
                dialogueText.text += textList[index][i];

                yield return new WaitForSeconds(textSpeed);
            }
            dialogueText.text += "\n";
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
