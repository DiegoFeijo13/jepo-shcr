using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DungeonLevelControl : ScriptableObject
{
    public int CurrentLevel = 1;
    public int DungeonWidth = 30;
    public int DungeonHeight = 30;

    private readonly int DungeonStartDimension = 50;

    public void UpdateRoomDimension()
    {
        int increment = LevelIncrement();

        DungeonWidth = DungeonStartDimension + increment;
        DungeonHeight = DungeonStartDimension + increment;
    }

    private int LevelIncrement()
    {
        int increment = 0;
        for (int i = CurrentLevel; i > 0; i--)
        {
            increment += i;
        }

        return increment;
    }
}
