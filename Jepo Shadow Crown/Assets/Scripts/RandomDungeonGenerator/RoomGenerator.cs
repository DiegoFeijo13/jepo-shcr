using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RoomGenerator
{
    private readonly int offset;
    private readonly RoomHelper roomHelper;

    public Dictionary<Vector2Int, Room> rooms;
    public List<Corridor> corridors;

    public HashSet<Vector2Int> floorPositions;
    public HashSet<Vector2Int> corridorPositions;

    public RoomGenerator(int offset = 0)
    {
        this.offset = offset;
        rooms = new Dictionary<Vector2Int, Room>();
        corridors = new List<Corridor>();
    }

    public void CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        floorPositions = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            var newRoom = new Room { 
                Center = (Vector2Int)Vector3Int.RoundToInt(room.center),
                Positions = new HashSet<Vector2Int>()
            };

            var size = (Vector2Int)Vector3Int.RoundToInt(room.size);
            var min = (Vector2Int)Vector3Int.RoundToInt(room.min);

            for (int col = offset; col <= size.x - offset; col++)
            {
                for (int row = offset; row <= size.y - offset; row++)
                {
                    Vector2Int position = min + new Vector2Int(col, row);
                    newRoom.Positions.Add(position);
                }
            }
            rooms.Add(newRoom.Center, newRoom);

            floorPositions.UnionWith(newRoom.Positions);
        }
    }

    public void ConnectRooms()
    {
        corridorPositions = new HashSet<Vector2Int>();
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var r in rooms)
        {
            roomCenters.Add(r.Key);
    }

    public HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        rooms[currentRoomCenter].Tag = "start";

        while (roomCenters.Count > 0)
        {

            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

            currentRoomCenter = closest;
            corridorPositions.UnionWith(newCorridor);
        }

        floorPositions.UnionWith(corridorPositions);
        }

    public void TagRooms()
    {
        RoomHelper.TagFurthestRoom(rooms, corridors, "exit");
        
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

        corridors.Add(new Corridor
        {
            RoomFrom = rooms[currentRoomCenter],
            RoomTo = rooms[destination],
            Length = corridor.Count
        });

        return corridor;
    }

    internal Room StartRoom()
    {
        if (!rooms.Any())
            return null;
        return rooms.Values.First(x => x.Tag == "start");
    }

    internal Room ExitRoom()
    {
        if (!rooms.Any())
            return null;
        return rooms.Values.First(x => x.Tag == "exit");
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
