using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DungeonLevelControl : ScriptableObject
{
    public int CurrentLevel = 1;
    public int DeviationRate = 1;
    public int MainPathRoomRate = 1;
    public int EnemiesPerRoom = 3;
    
    private readonly int _initialLevel = 1;
    private readonly int _initialDeviationRate = 1;
    private readonly int _initialMainPathRoomRate = 1;
    private readonly int _initialEnemiesPerRoom = 3;

    private void OnEnable()
    {
        CurrentLevel = _initialLevel;
        DeviationRate = _initialDeviationRate;
        MainPathRoomRate = _initialMainPathRoomRate;
        EnemiesPerRoom = _initialEnemiesPerRoom;
    }

    private void Reset()
    {
        CurrentLevel = _initialLevel;
        DeviationRate = _initialDeviationRate;
        MainPathRoomRate = _initialMainPathRoomRate;
        EnemiesPerRoom = _initialEnemiesPerRoom;
    }

    internal void LevelUp()
    {
        CurrentLevel++;
        MainPathRoomRate ++;
        EnemiesPerRoom = Math.Max(_initialEnemiesPerRoom, (int)Math.Floor(CurrentLevel / 2m));
    }
}
