using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    // FindWallsInDirection으로 구한 벽 위치값들을 가지고 타일맵에 칠해줌
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPos = FindWallsInDirection(floorPos, Direction2D.cardinalDirectionList);

        foreach (var pos in basicWallPos)
        {
            tilemapVisualizer.PaintSingleBasicWall(pos);
        }
    }
    // 이중 반복문을 사용하여 floorPos위치에서 4방향을 검사하고 같지 않을시 HashSet에 넣어줌
    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighbourPos = pos + direction;
                if (!floorPos.Contains(neighbourPos))
                    wallPos.Add(neighbourPos);
            }
        }
        return wallPos;
    }
}
