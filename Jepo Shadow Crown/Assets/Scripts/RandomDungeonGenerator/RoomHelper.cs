using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public class RoomHelper
{
    public Vector2Int? startPos { get; set; }

    private readonly List<RoomNode> roomNodes;
    private Dictionary<Vector2Int, int> roomDistances;

    public RoomHelper()
    {
        roomNodes = new List<RoomNode>();
        roomDistances = new Dictionary<Vector2Int, int>();
    }

    public void AddNode(RoomNode node) => roomNodes.Add(node);

    public void CalculateRooms()
    {
        Validate();

        var startnodes = roomNodes.Where(x => x.from == startPos.Value).ToList();
        foreach (var node in startnodes)
        {
            NextNode(node.to, node.distance);
        }
    }

    public Vector2Int FindFurthestRoom()
    {
        if (!roomDistances.Any())
            CalculateRooms();

        int maxDistance = roomDistances.Max(y => y.Value);

        return roomDistances.Where(x => x.Value == maxDistance).First().Key;

    }

    private void Validate()
    {
        if (startPos == null)
            throw new ArgumentException("Start position is null");

        if (!roomNodes.Any())
            throw new ArgumentException("No rooms to interact!");

        if (!roomNodes.Any(x => x.from == startPos.Value))
            throw new ArgumentException("No node from start position");
    }

    private void AddOrUpdateRoom(Vector2Int roomCenter, int distanceFromStart)
    {
        if (roomDistances.TryGetValue(roomCenter, out int distance))
        {
            if (distanceFromStart < distance)
                roomDistances[roomCenter] = distanceFromStart;
        }
        else
        {
            roomDistances.Add(roomCenter, distanceFromStart);
        }


    }

    private void NextNode(Vector2Int center, int distance)
    {
        AddOrUpdateRoom(center, distance);

        var nextNodes = roomNodes.Where(x => x.from == center).ToList();

        if (nextNodes.Any())
        {
            foreach (var node in nextNodes)
            {
                NextNode(node.to, node.distance + distance);
            }
        }
    }


}

public class RoomNode
{
    public Vector2Int from { get; set; }
    public Vector2Int to { get; set; }
    public int distance { get; set; }

    public int distanceFromStart { get; set; }
}


