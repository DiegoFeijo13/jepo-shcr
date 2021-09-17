﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomParent : MonoBehaviour
{
    public int Width;
    public int Height;
    public int X;
    public int Y;
    public string UpExit;
    public string LeftExit;
    public string DownExit;
    public string RightExit;

    public EnemyBase[] Enemies;

    private void Start()
    {
        if(RoomManager.Instance == null)
        {
            Debug.LogWarning("Pressed play in a room and not in the main scene");
            return;
        }

        RoomManager.Instance.RegisterRoom(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
        //Gizmos.DrawLine(transform.position, new Vector3(Width / 2, 0, 0));
        //Gizmos.DrawLine(transform.position, new Vector3(-(Width / 2), 0, 0));
        //Gizmos.DrawLine(transform.position, new Vector3(0, Height / 2, 0));
        //Gizmos.DrawLine(transform.position, new Vector3(0, -(Height / 2), 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var playerControl = collider.gameObject.GetComponent<PlayerControl>();
            playerControl.SetFrozen(true, false);
            RoomManager.Instance.OnPlayerEnterRoom(this);
            //Espera 1s
            new WaitForSeconds(1);
            //
            playerControl.SetFrozen(false, false);
        }
    }

    public void ActivateRoom()
    {
        if(Enemies != null && Enemies.Length > 0)
        {
            foreach (var enemy in Enemies)
            {
                enemy.FullRestore();
            }
        }
    }
}
