using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int Center;
    public HashSet<Vector2Int> Positions;
    public string Tag;
    public int DistanceFromStart;
}
