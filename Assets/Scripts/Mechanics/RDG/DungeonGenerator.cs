using Assets.Scripts.Mechanics.RDG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Dungeon Stuff")]
    [SerializeField] private DungeonLevelControl levelControl;
    [SerializeField] private EnemiesDatabase enemiesDB;
    [SerializeField] private GameObject portal;

    [Header("Room Stuff")]
    [SerializeField] private GameObject roomPrefab;
    [SerializeField][Range(3, 10)] private int roomRadius = 5;

    [Header("Tiles")]
    [SerializeField] private Tile[] floorTiles;
    [SerializeField] private Tile topWallTile;
    [SerializeField] private Tile rightWallTile;
    [SerializeField] private Tile botWallTile;
    [SerializeField] private Tile leftWallTile;

    private DungeonConfig _dngConfig;
    private DungeonCursor _cursor;
    private List<Vector3> _spawnPositions;
    
    private void Awake()
    {
        _dngConfig = new DungeonConfig()
        {
            RoomRadius = roomRadius,
            FloorTiles = floorTiles,
            TopWallTile = topWallTile,
            RightWallTile = rightWallTile,
            BotWallTile = botWallTile,
            LeftWallTile = leftWallTile
        };

        _cursor = new(_dngConfig.StepSize());
        _spawnPositions = new List<Vector3>();

    }

    internal void Run()
    {
        LoadStartRoom();
        BuildMainPath();
        LoadExitRoom();
        if (levelControl.DeviationRate > 1)
            BuildAlternativePath();
        CloseRooms();
    }

    private void LoadStartRoom()
    {

        _cursor.Position = Vector3.zero;

        var room = CreateRoom(hasEnemies:false);
        UpdatePositions(room);
    }

    private void BuildMainPath()
    {
        CleanUpPositions();
        int pathRooms = 0;
        int failChances = 1000;

        while (pathRooms < levelControl.MainPathRoomRate)
        {
            _cursor.Move();

            //Generate room
            var room = CreateRoom();

            if (room == null)
            {
                failChances--;
                if (failChances <= 0)
                    break;

                continue;
            }

            UpdatePositions(room);

            pathRooms++;
        }
    }

    private void LoadExitRoom()
    {
        CleanUpPositions();

        _cursor.Move();

        var room = CreateRoom();
        if(room == null)
        {
            Debug.Log("Could not load Exit Room");
            return;
        }

        UpdatePositions(room);

        Instantiate(portal,new Vector3(_cursor.Position.x + 0.5f, _cursor.Position.y + 0.5f), Quaternion.identity);
    }

    private void BuildAlternativePath()
    {
        CleanUpPositions();

        if (_spawnPositions.Count == 0)
            return;

        //Given the deviation, loop and generate a room for each position
        int initialDeviation = 0;
        while (initialDeviation < levelControl.DeviationRate)
        {
            List<Vector3> newPositions = new();

            foreach (var pos in _spawnPositions)
            {
                _cursor.Position = pos;
                var room = CreateRoom();
                if (room != null)
                    newPositions.AddRange(room.GetExitPositions(_dngConfig.StepSize()));
            }

            _spawnPositions.AddRange(newPositions);

            initialDeviation++;
            CleanUpPositions();
        }
    }

    private void CloseRooms()
    {
        CleanUpPositions();

        if (_spawnPositions.Count == 0)
            return;

        foreach (var pos in _spawnPositions)
        {
            _cursor.Position = pos;
            CreateRoom(true);
        }
    }

    private Room CreateRoom(bool isDeadEnd = false, bool hasEnemies = true)
    {
        if (RoomManager.GetRoom(_cursor.Position) != null)
        {
            Debug.Log("Cannot generate room because another room already exists in this position." + _cursor.Position);
            return null;
        }

        var roomExits = RoomManager.GetRoomExits(_cursor.Position, _dngConfig.StepSize(), isDeadEnd);
        if (roomExits == RoomExits.None)
        {
            Debug.Log("Could not resolve the room exits." + _cursor.Position);
            return null;
        }

        var newRoom = Instantiate(roomPrefab, _cursor.Position, Quaternion.identity);
        var roomGenerator = newRoom.GetComponent<Room>();
        roomGenerator.FillRoom(roomExits, _dngConfig, hasEnemies);

        return roomGenerator;
    }

    private void UpdatePositions(Room room)
    {
        var positions = room.GetExitPositions(_dngConfig.StepSize());

        _spawnPositions.AddRange(positions);
    }

    private void CleanUpPositions()
    {
        if (_spawnPositions.Count == 0)
            return;

        _spawnPositions.RemoveAll(sp => RoomManager.GetRoom(sp) != null);
    }
}
