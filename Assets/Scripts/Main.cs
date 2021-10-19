using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main main;
    public enum BulletType{bullet, shell, laser};

    void Awake() 
    {
        main = this;   
    }

    public static Vector2 mousePos2D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return (Vector2)mousePos;
    }
}
