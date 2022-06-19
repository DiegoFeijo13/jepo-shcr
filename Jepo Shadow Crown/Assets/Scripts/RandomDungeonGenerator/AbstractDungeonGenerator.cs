using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon(bool fromEditor = false)
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration(fromEditor);
    }

    protected abstract void RunProceduralGeneration(bool fromEditor = false);
}
