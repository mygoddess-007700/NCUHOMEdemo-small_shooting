using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grapple : MonoBehaviour
{
    public enum eMode{none, gOut, gInMiss, gInHit}

    [Header("Set In Inspector")]
    public float grappleSpeed = 10;
    public float grappleLength = 4;
    public float grappleInLength = 0.5f;
    public int unsafeTileHealthPenalty = 2;
    public GUIPanel gui;

    [Header("Set Dynamically")]
    public eMode mode = eMode.none;

    private Hero hero;
    private Rigidbody rigid;
    private Collider drayColld;

    private GameObject grapHead;
    private LineRenderer grapLine;
    private Vector3 p0, p1;
    private Vector3 dir;

    void Awake() 
    {
        hero = GetComponent<Hero>();
        rigid = GetComponent<Rigidbody>();
        drayColld = GetComponent<Collider>();

        Transform trans = transform.Find("Grappler");
        grapHead = trans.gameObject;
        grapLine = grapHead.GetComponent<LineRenderer>();
        grapHead.SetActive(false);
    }

    void Update() 
    {
        if(!hero.hasGrappler)
            return;

        switch(mode)
        {
            case eMode.none:
                //如果按下抓取键
                if(gui.grapplerButtonIsPressed)
                {
                    dir = hero.dir;
                    StartGrapple();
                }
                break;
        }
    }

    void StartGrapple()
    { 
        gui.grapplerButtonIsPressed = false;
        rigid.velocity = Vector3.zero;

        grapHead.SetActive(true);

        p0 = transform.position + (dir * 0.5f);
        p1 = p0;
        grapHead.transform.position = p1;
        if(dir.x >= 0)
        {
            grapHead.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));
        }
        else if(dir.x < 0)
        {
            grapHead.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI)+180);
        }

        grapLine.positionCount = 2;
        grapLine.SetPosition(0, p0);
        grapLine.SetPosition(1, p1);
        mode = eMode.gOut;
    }

    void FixedUpdate() {
        switch(mode)
        {
            case eMode.gOut: //发射抓取器
                p1 += dir * grappleSpeed * Time.fixedDeltaTime;
                grapHead.transform.position = p1;
                grapLine.SetPosition(1, p1);

                //确认抓取器是否击中物体
                if(p1.x > 23 && p1.x < 25 && p1.y >30)
                {
                    //将撞击可抓取的图块
                    mode = eMode.gInHit;
                    break;
                }
                if((p1-p0).magnitude >= grappleLength)
                {
                    //抓取器到最大长度并且未碰到任何物体
                    mode = eMode.gInMiss;
                }
                break;
            
            case eMode.gInMiss: //抓取器消失，以双倍速度返回
                p1 -= dir*2*grappleSpeed*Time.fixedDeltaTime;
                if(Vector3.Dot((p1-p0), dir) > 0)
                {
                    //抓取器仍然在Dray前面
                    grapHead.transform.position = p1;
                    grapLine.SetPosition(1, p1);
                }
                else
                {
                    StopGrapple();
                }
                break;

            case eMode.gInHit: //抓取器碰到物体，将Dray推到墙
                float dist = grappleInLength + grappleSpeed*Time.fixedDeltaTime;
                if(dist > (p1-p0).magnitude)
                {
                    p0 = p1 - (dir * grappleInLength);
                    transform.position = p0;
                    SceneManager.LoadScene("SceneFinish");
                    // StopGrapple();
                    break;                  
                }
                p0 += dir * grappleSpeed * Time.fixedDeltaTime;
                transform.position = p0;
                grapLine.SetPosition(0, p1); 
                grapHead.transform.position = p1;
                break;
        }
    }

    void StopGrapple()
    {
        grapHead.SetActive(false);

        mode = eMode.none;
    }

    // void OnTriggerEnter(Collider colld) 
    // {
    //     Enemy e = colld.GetComponent<Enemy>();
    //     if(e == null)
    //     {
    //         return;
    //     }
        
    //     mode = eMode.gInMiss;
    // }
}
