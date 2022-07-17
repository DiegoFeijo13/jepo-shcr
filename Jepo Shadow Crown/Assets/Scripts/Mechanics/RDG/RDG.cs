using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RDG : MonoBehaviour
{

    [SerializeField] private Tile groundTile;
    [SerializeField] private Tile pitTile;
    [SerializeField] private Tile topWallTile;
    [SerializeField] private Tile botWallTile;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap pitMap;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemiesDataBase enemiesDB;
    [SerializeField] private DungeonLevelControl levelControl;
    [SerializeField] private GameObject torch;
    [SerializeField] private GameObject nextLevelPortal;

    private int routeCount = 0;
    private List<GameObject> torches = new List<GameObject>();

    private void Start()
    {
        Run();
    }

    private void Run()
    {
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                        SpawnTorch(pos);
                    }
                    else if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, botWallTile);
                    }
                }
            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < levelControl.MaxRoutes)
        {
            routeCount++;
            while (++routeLength < levelControl.MaxRouteLength)
            {
                
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= levelControl.RoomRate)
                    roomSize = Random.Range(3, 6);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= levelControl.DeviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, levelControl.MaxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= levelControl.DeviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, levelControl.MaxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= levelControl.DeviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, levelControl.MaxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        HashSet<Vector3Int> positions = new HashSet<Vector3Int>();

        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                positions.Add(tilePos);

                groundMap.SetTile(tilePos, groundTile);
            }
        }

        //If not a hallway
        if(radius > 1)        
            SpawnEnemies(positions);        

        PositionPortal(x,y);
    }

    private void PositionPortal(int x, int y)
    {
        if (nextLevelPortal == null)
            return;

        nextLevelPortal.transform.position = new Vector3(x + 0.5f, y + 0.5f);
    }

    private void SpawnEnemies(HashSet<Vector3Int> positions)
    {
        if (enemiesDB == null || levelControl == null)
            return;

        int maxEnemies = Random.Range(levelControl.CurrentLevel - 1, levelControl.CurrentLevel + 1);

        int spawnedEnemies = 0;        
        var enemies = enemiesDB.GetEnemies(levelControl.CurrentLevel);
        
        if ((enemies?.Count).GetValueOrDefault() == 0)
            return;

        while(spawnedEnemies < maxEnemies)
        {
            int rndPos = Random.Range(1, positions.Count);
            int rndEnemy = Random.Range(0, enemies.Count);
            var pos = positions.ElementAt(rndPos);
            var v3pos = new Vector3(pos.x, pos.y);
            var enemy = enemies.ElementAt(rndEnemy);
            
            var enemyObject = Instantiate(enemy, v3pos, this.gameObject.transform.rotation);

            spawnedEnemies++;
        }

    }

    private void SpawnTorch(Vector3Int position)
    {
        if (torch == null || levelControl == null)
            return;

        //Chance of spawn a torch based on dungeon level. Higher the level, lower the chance
        int level = levelControl.CurrentLevel;
        int chance = Random.Range(0, level+1);
        if(chance / level < 0.2f)
        {
            var v3pos = new Vector3(position.x+0.5f, position.y + 0.5f);
            //Check if there is an adjacent torch
            if (torches.Any(x => x.transform.position == v3pos - Vector3.left || x.transform.position == v3pos - Vector3.right))
                return;

            var torchObject = Instantiate(torch, v3pos, this.gameObject.transform.rotation);

            torches.Add(torchObject);
        }

    }

    private void ClearData()
    {
        routeCount = 0;
        ClearEnemies();
        ClearObjects();
        groundMap.ClearAllTiles();
        wallMap.ClearAllTiles();
        pitMap.ClearAllTiles();
    }

    private void ClearObjects()
    {
        var objects = GameObject.FindGameObjectsWithTag("Objects");

        if (!objects.Any())
            return;

        foreach (var o in objects)
        {
            Destroy(o);

        }

        torches = new List<GameObject>();
    }

    private void ClearEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!enemies.Any())
            return;

        foreach (var e in enemies)
        {
            Destroy(e);
        }
    }

    private void PositionHero()
    {
        player.transform.position = new Vector3(0, 0);
    }

    internal void NextLevel()
    {
        levelControl.LevelUp();
        ClearData();
        PositionHero();
        Run();
    }

    
}