using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractdungeonGenerator : MonoBehaviour
{
    [SerializeField]
    public TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    public Vector2Int startposition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    public void ClearDungeon()
    {
        tilemapVisualizer.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
