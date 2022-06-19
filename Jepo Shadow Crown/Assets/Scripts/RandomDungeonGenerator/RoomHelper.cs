using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public static class RoomHelper
{
    public static void TagFurthestRoom(Dictionary<Vector2Int, Room> rooms, List<Corridor> corridors, string tag)
    {
        CalculateRooms(rooms, corridors);

        int maxDistance = rooms.Values.Max(x => x.DistanceFromStart);
        rooms.Values.First(x => x.DistanceFromStart == maxDistance).Tag = tag;
    }

    private static void CalculateRooms(Dictionary<Vector2Int, Room> rooms, List<Corridor> corridors)
    {
        Validate(rooms);

        var startnodes = corridors.Where(x => x.RoomFrom.Tag == "start").ToList();
        var unvisitedNodes = corridors.Where(x => !startnodes.Contains(x)).ToList();
        foreach (var node in startnodes)
        {
            NextNode(node.RoomTo.Center, node.Length, unvisitedNodes, rooms);
        }
    }

    private static void NextNode(Vector2Int center, int distance, List<Corridor> unvisited, Dictionary<Vector2Int, Room> rooms)
    {
        //Update room
        var roomToUpdate = rooms[center];
        if (distance < roomToUpdate.DistanceFromStart)
            roomToUpdate.DistanceFromStart = distance;

        var nextNodes = unvisited.Where(x => x.RoomFrom.Center == center).ToList();
        unvisited = unvisited.Except(nextNodes).ToList();

        if (nextNodes.Any())
        {
            foreach (var node in nextNodes)
            {
                NextNode(node.RoomTo.Center, node.Length + distance, unvisited, rooms);
            }
        }
    }

    private static void Validate(Dictionary<Vector2Int, Room> rooms)
    {
        if (!rooms.Any())
            throw new ArgumentException("No rooms to interact!");

        if (!rooms.Values.Any(x => x.Tag == "start"))
            throw new ArgumentException("No start room");
    }
}

public class RoomNode
{
    public Vector2Int from { get; set; }
    public Vector2Int to { get; set; }
    public int distance { get; set; }

    public int distanceFromStart { get; set; }
}


