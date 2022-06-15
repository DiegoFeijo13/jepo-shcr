using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var wallPositions = FindWallsInDirections(floorPositions, Direction2D.eightDirectionList);

        tilemapVisualizer.PaintWallTiles(wallPositions);
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var nPos = position + direction;
                if (!floorPositions.Contains(nPos))
                    wallPositions.Add(nPos);
            }
        }

        return wallPositions;
    }
}
