using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomGenerator
{
    private readonly int offset;
    private readonly RoomHelper roomHelper;

    public RoomGenerator(int offset = 0)
    {
        this.offset = offset;
        roomHelper = new RoomHelper();
    }

    public HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        var floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);

                }
            }
        }

        return floor;
    }

    public HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        roomHelper.startPos = currentRoomCenter;

        while (roomCenters.Count > 0)
        {

            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
                position += Vector2Int.up;
            else if (destination.y < position.y)
                position += Vector2Int.down;

            corridor.Add(position);
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
                position += Vector2Int.right;
            else if (destination.x < position.x)
                position += Vector2Int.left;

            corridor.Add(position);
        }

        roomHelper.AddNode(new RoomNode
        {
            from = currentRoomCenter,
            to = destination,
            distance = corridor.Count
        });

        return corridor;
    }

    internal Vector2Int? FindFurthestRoom()
    {
        return roomHelper.FindFurthestRoom();
    }

    internal Vector2Int? GetStartPos()
    {
        return roomHelper.startPos;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (var pos in roomCenters)
        {
            float currentDistance = Vector2.Distance(pos, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = pos;
            }
        }

        return closest;
    }
}
