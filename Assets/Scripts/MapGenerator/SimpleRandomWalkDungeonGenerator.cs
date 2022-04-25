using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{


    [SerializeField] protected SimpleRandomWalkSO randomWalkParameterts;


    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2> floorPositions = RunRandomWalk(randomWalkParameterts, startPos);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }
    // RandomWalk 지점을 랜덤으로 줌 
    // True 일 시 시작 지점을 for문 돌때마다 랜덤으로 주어 조금더 랜덤성 있는 지형이 생성됨
    // false일 시 시작 지점이 Start지점이라 중심 지점이 높은 확률로 체워져 섬과 같은 형식이 됨
    protected HashSet<Vector2> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2 position)
    {
        var curPos = position;
        HashSet<Vector2> floorPositions = new HashSet<Vector2>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProveduralGenerationAlgorithms.SimpleRandomWalk(curPos, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
                curPos = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
