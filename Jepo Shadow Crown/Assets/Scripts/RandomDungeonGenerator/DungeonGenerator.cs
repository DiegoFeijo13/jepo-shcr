using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool hasExitRoom;
    [SerializeField]
    private GameObject Hero;

    private Dictionary<Vector2Int, BoundsInt> roomsDictionary = new Dictionary<Vector2Int, BoundsInt>();

    private RoomGenerator roomGenerator;

    private Vector2Int? startPos;
    private Vector2Int? bossPos;

    private void Awake()
    {
        RunProceduralGeneration();
    }

    private void OnDrawGizmosSelected()
    {
        float radius = 5f;
        if (startPos.HasValue)
        {
            Gizmos.color = new Color(0,1,0, 0.3f);
            var pos = new Vector3Int(startPos.Value.x, startPos.Value.y);
            Gizmos.DrawSphere(pos, radius);
        }

        if (bossPos.HasValue)
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            var pos = new Vector3Int(bossPos.Value.x, bossPos.Value.y);
            Gizmos.DrawSphere(pos, radius);
        }
    }

    protected override void RunProceduralGeneration()
    {
        Validate();
        CreateRooms();
    }

    private void Validate()
    {
        if (Hero == null)
            throw new Exception("Hero cannot be null");

    }

    private void CreateRooms()
    {
        ClearData();

        var rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)),
            minRoomWidth,
            minRoomHeight);

        SaveRoomsData(rooms);

        var floor = roomGenerator.CreateSimpleRooms(rooms);

        List<Vector2Int> roomCenters = rooms.Select(x => (Vector2Int)Vector3Int.RoundToInt(x.center)).ToList();

        var corridors = roomGenerator.ConnectRooms(roomCenters);

        startPos = roomGenerator.GetStartPos();
        bossPos = roomGenerator.FindFurthestRoom();

        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);

        PositionHero();
    }

    private void PositionHero()
    {
        Hero.transform.position = new Vector3(startPos.Value.x, startPos.Value.y, Hero.transform.position.z);
    }

    private void SaveRoomsData(List<BoundsInt> rooms)
    {
        foreach (var room in rooms)
        {
            //Save room data
            var roomCenter = (Vector2Int)Vector3Int.RoundToInt(room.center);
            roomsDictionary.Add(roomCenter, room);
        }
    }

    private void ClearData()
    {
        roomGenerator = new RoomGenerator(offset);        
        roomsDictionary = new Dictionary<Vector2Int, BoundsInt>();
        tilemapVisualizer.Clear();
    }

   



    
}
