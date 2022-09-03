using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DungeonLevelControl : ScriptableObject
{
    public int CurrentLevel = 1;
    public int DeviationRate = 10;
    public int RoomRate = 15;
    public int MaxRouteLength = 50;
    public int MaxRoutes = 10;    
    
    private readonly int _initialLevel = 1;
    private readonly int _initialDeviationRate = 5;
    private readonly int _initialRoomRate = 15;
    private readonly int _initialMaxRouteLength = 50;
    private readonly int _initialMaxRoutes = 20;

    private void OnEnable()
    {
        CurrentLevel = _initialLevel;
        DeviationRate = _initialDeviationRate;
        RoomRate = _initialRoomRate;
        MaxRouteLength = _initialMaxRouteLength;
        MaxRoutes = _initialMaxRoutes;
    }

    private void Reset()
    {
        CurrentLevel = _initialLevel;
        DeviationRate = _initialDeviationRate;
        RoomRate = _initialRoomRate;
        MaxRouteLength = _initialMaxRouteLength;
        MaxRoutes = _initialMaxRoutes;
    }

    internal void LevelUp()
    {
        CurrentLevel++;
        //DeviationRate += CurrentLevel;
        //RoomRate += CurrentLevel;
        MaxRouteLength += CurrentLevel * 2;
        //MaxRoutes += CurrentLevel;
    }
}
