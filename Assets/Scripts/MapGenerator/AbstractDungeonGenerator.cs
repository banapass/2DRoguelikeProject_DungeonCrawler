using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer tilemapVisualizer;
    [SerializeField] protected Vector2 startPos = Vector2.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }
    virtual protected void Awake()
    {
        GenerateDungeon();
    }
    public void DungeonClear()
    {
        tilemapVisualizer.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
