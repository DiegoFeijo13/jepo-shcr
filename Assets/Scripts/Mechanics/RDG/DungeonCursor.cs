using Assets.Scripts.Mechanics.RDG;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonCursor
{
    internal Vector3 Position { get; set; }
    private int _stepSize;

    public DungeonCursor(int stepSize) { _stepSize = stepSize; }

    internal void Move()
    {
        var room = RoomManager.GetRoom(Position);
        if(room == null)
        {
            Debug.Log("Cannot resolve direction to move. There is no room overlaping the spawn point.");
            return;
        }

        var directions = new List<Vector2>();

        Vector3 position;

        if (room.GetRoomExits().HasTopExit())
        {
            position = new Vector3(Position.x, Position.y + _stepSize);            
            if (RoomManager.GetRoom(position) == null)
                directions.Add(Vector2.up);
        }

        if (room.GetRoomExits().HasRightExit())
        {
            position = new Vector3(Position.x + _stepSize, Position.y);
            if (RoomManager.GetRoom(position) == null)
                directions.Add(Vector2.right);
        }

        if (room.GetRoomExits().HasBottomExit())
        {
            position = new Vector3(Position.x, Position.y - _stepSize);
            if (RoomManager.GetRoom(position) == null)
                directions.Add(Vector2.down);
        }

        if (room.GetRoomExits().HasLeftExit())
        {
            position = new Vector3(Position.x - _stepSize, Position.y);
            if (RoomManager.GetRoom(position) == null)
                directions.Add(Vector2.left);
        }

        if(directions.Count == 0)
        {
            Debug.Log("Could not resolve a direction to move.");
            return;
        }

        MoveTo(directions[Random.Range(0, directions.Count)]);
    }

    private void MoveTo(Vector2 newDir)
    {
        if (newDir == Vector2.up)
            Position += new Vector3(0, _stepSize);
        if (newDir == Vector2.right)
            Position += new Vector3(_stepSize, 0);
        if (newDir == Vector2.down)
            Position -= new Vector3(0, _stepSize);
        if (newDir == Vector2.left)
            Position -= new Vector3(_stepSize, 0);

    }    
}
