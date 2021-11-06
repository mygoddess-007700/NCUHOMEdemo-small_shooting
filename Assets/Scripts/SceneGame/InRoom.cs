using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoom : MonoBehaviour
{
    static public float ROOM_W = 16;
    static public float ROOM_H = 11;
    static public int xRoom = 3;
    static public int yRoom = 2;

    //角色在当前房间里的坐标位置
    public Vector2 RoomPos //也设置成二维，用来配合RoomNum，z为0
    {
        get{
            Vector2 tPos = transform.position;
            tPos.x %= ROOM_W;
            tPos.y %= ROOM_H;
            return tPos;
        }
        set{
            Vector2 rm = RoomNum;
            rm.x *= ROOM_W;
            rm.y *= ROOM_H;
            rm += value;
            transform.position = rm;
        }
    }

    //角色在的房间（不算当前这个房间）(相当于二维数组)
    public Vector2 RoomNum
    {
        get{
            Vector2 tPos = transform.position;
            tPos.x = Mathf.Floor(tPos.x / ROOM_W);
            tPos.y = Mathf.Floor(tPos.y / ROOM_H);
            return tPos;
        }
        set{
            Vector2 rPos = RoomPos;
            Vector2 rm = value;
            rm.x *= ROOM_W;
            rm.y *= ROOM_H;
            transform.position = rm + rPos;
        }
    }
}
