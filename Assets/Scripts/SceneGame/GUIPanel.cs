using System.Net.Mime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIPanel : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Hero hero;
    public Sprite healthEmpty;
    public Sprite healthFull;
    public Image pistolPanelImage;
    public InRoom heroInRoom;
    public GameObject grappler;
    public Text goalText;
    public Text bulletNumText;
    public Text roomNumText;
    public Button outingButton;
    public float smooth = 0.5f;
    public int roomNum = 1;
    public Button grapplerButton;
    public bool grapplerButtonIsPressed = false;

    private List<Image> healthImages;
    public Color pistolPanelImageColor;

    void Start() 
    {
        //得分面板
        // Transform trans;
        // Transform goalPanel = transform.Find("GoalPanel");
        // if(goalPanel != null)
        // {
        //     trans = goalPanel.Find("Goal");
        //     goalText = trans.GetComponent<Text>();
        // }
        //生命值面板
        Transform trans;
        Transform healthPanel = transform.Find("HealthPanel");
        healthImages = new List<Image>();
        if(healthPanel != null)
        {
            for(int i=0; i<20; i++)
            {
                trans = healthPanel.Find("H"+i); 
                if(trans == null) 
                    break;
                healthImages.Add(trans.GetComponent<Image>());
            }
        }
        pistolPanelImageColor = pistolPanelImage.color;
        //子弹数面板
        // Transform bulletPanel = transform.Find("BulletPanel");
        // if(bulletPanel != null)
        // {
        //     trans = bulletPanel.Find("BulletNum");
        //     bulletNumText = trans.GetComponent<Text>();
        // }
        //房间数面板
        // Transform roomPanel = transform.Find("RoomPanel");
        // if(roomPanel != null)
        // {
        //     trans = roomPanel.Find("RoomNum");
        //     roomNumText = trans.GetComponent<Text>();
        // }
        grappler.SetActive(false);
        grapplerButton.onClick.AddListener(IsPressed);
        //退出按钮
        outingButton.onClick.AddListener(outingButtonClick);
        // Transform otherPanel = transform.Find("OtherPanel");
        // if(otherPanel != null)
        // {
        //     foreach(string buttonName in buttonsName)
        //     {
        //         GameObject buttonObject = GameObject.Find(buttonName);
        //         if(buttonObject == null)
        //             continue;
        //         Button btn = buttonObject.GetComponent<Button>();
        //         if(buttonName == "Reload")
        //             btn.onClick.AddListener(reloadButtonClick);
        //     }
        // }
    }

    void Update() 
    {
        //显示得分
        goalText.text = "your goal\n"+hero.score.ToString();
        // if(game == 0)
        // {
        //     goalText.text = "your goal\n"+hero.score.ToString();
        // }
        // else if(game == 1)
        // {
        //     goalText.text = "Congratulate\n"+"You finished";
        // }
        // else
        // {
        //     goalText.text = "So Sad\n"+"You die";
        // }
        if(hero.hasGrappler)
        {
            grappler.SetActive(true);
        }
        //显示生命值
        int health = hero.Health;

        for(int i=0; i<healthImages.Count; i++)
        {
            if(health >= 1)
            {
                healthImages[i].sprite = healthFull;
            }
            else
            {
                healthImages[i].sprite = healthEmpty;
            }
            health -= 1;
        }
        //显示子弹数
        bulletNumText.text = "×"+hero.bulletNum.ToString();
        if(0 == hero.bulletNum)
        {
            // pistolPanelImage.color = Color.Lerp(pistolPanelImageColor, Color.black, smooth*Time.deltaTime);
            pistolPanelImage.color = Color.black;
        }
        else
        {
            pistolPanelImage.color = pistolPanelImageColor;
        }
        //显示房间数
        roomNumText.text = "Room："+roomNum.ToString();
    }

    void outingButtonClick()
    {
        SceneManager.LoadScene("SceneBegin");
    }

    void IsPressed()
    {
        grapplerButtonIsPressed = true;
    }
}