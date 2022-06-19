using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 7, minRoomHeight = 7;
    [SerializeField] private GameObject Hero;
    [SerializeField] private DungeonLevelControl LevelControl;
    [SerializeField] private EnemiesDataBase EnemiesDB;
    [SerializeField] private GameObject Torch;

    private int dungeonWidth = 50, dungeonHeight = 50;
    public int offset = 2;

    private RoomGenerator roomGenerator;

    private List<GameObject> Enemies = new List<GameObject>();
    private List<GameObject> Torches = new List<GameObject>();

    private void Awake()
    {
        RunProceduralGeneration();
    }

    private void OnDrawGizmosSelected()
    {
        if (roomGenerator == null)
            return;

        var startRoom = roomGenerator.StartRoom();

        if (startRoom != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f);
            foreach (var tile in startRoom.Positions)
            {
                Gizmos.DrawCube(new Vector3(tile.x, tile.y), Vector3.one);
        }
        }

        var exitRoom = roomGenerator.ExitRoom();
        if (exitRoom != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            foreach (var tile in exitRoom.Positions)
            {
                Gizmos.DrawCube(new Vector3(tile.x, tile.y), Vector3.one);
            }
        }
    }

    protected override void RunProceduralGeneration(bool fromEditor = false)
    {
        Validate();
        CreateRooms();
        PositionHero();
        if (!fromEditor)
            FillRooms();
    }

    private void CreateRooms()
    {
        ClearData();
        LevelControl.UpdateRoomDimension();

        this.dungeonHeight = LevelControl.DungeonHeight;
        this.dungeonWidth = LevelControl.DungeonWidth;

        var rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)),
            minRoomWidth,
            minRoomHeight);

        roomGenerator.CreateSimpleRooms(rooms);

        roomGenerator.ConnectRooms();

        tilemapVisualizer.PaintFloorTiles(roomGenerator.floorPositions);

        WallGenerator.CreateWalls(roomGenerator.floorPositions, tilemapVisualizer);

        roomGenerator.TagRooms();
    }

    private void PositionHero()
    {
        var startPos = roomGenerator.StartRoom().Center;
        Hero.transform.position = new Vector3(startPos.x, startPos.y, Hero.transform.position.z);
    }

    private void FillRooms()
    {
        var rooms = roomGenerator.rooms;
        if (!rooms.Any())
            return;

        string[] safeTags = { "start", "exit" };
        foreach (var room in rooms)
        {
            if (Torch != null)
                PlaceTorchs(room.Value.Positions);

            if (safeTags.Contains(room.Value.Tag))
                continue;

            PlaceEnemies(room.Value.Positions);
        }
    }

    private void PlaceEnemies(HashSet<Vector2Int> room)
    {
        //Enemies per room
        int minEnemies = 2;
        int maxEnemies = 5;
        var placementHelper = new EnemyPlacementHelper();

        var enemyPositions = placementHelper.PlaceEnemies(EnemiesDB, LevelControl.CurrentLevel, room, minEnemies, maxEnemies);

        foreach (var ep in enemyPositions)
        {
            var enemy = Instantiate(ep.Enemy, ep.Position, this.gameObject.transform.rotation);
            Enemies.Add(enemy);
        }
    }

    private void PlaceTorchs(HashSet<Vector2Int> room)
    {
        int minTorches = 1;
        int maxTorches = 3;
        int numTorches = Random.Range(minTorches, maxTorches);
        var placementHelper = new ItemPlacementHelper(roomGenerator.floorPositions, room);

        for (int i = 0; i < numTorches; i++)
        {
            var position = placementHelper.GetItemPlacementPosition(PlacementType.NearWall, 1000, Vector2Int.one, false);
            if (position.HasValue)
            {
                var torch = Instantiate(Torch, position.Value, this.gameObject.transform.rotation);

                Torches.Add(torch);
            }
        }

        PositionHero();
    }
    private void DestroyEnemies()
    {
        if (!Enemies.Any())
            return;

        foreach (var e in Enemies)
    {
            Destroy(e);
        }

        Enemies = new List<GameObject>();
    }

    private void DestroyTorches()
    {
        if (!Torches.Any())
            return;

        foreach (var t in Torches)
        {
            Destroy(t);
        }

        Torches = new List<GameObject>();
    }

    private void ClearData()
    {
        roomGenerator = new RoomGenerator(offset);        
        roomsDictionary = new Dictionary<Vector2Int, BoundsInt>();
        tilemapVisualizer.Clear();
        DestroyEnemies();
        DestroyTorches();
    }

   



    
}
