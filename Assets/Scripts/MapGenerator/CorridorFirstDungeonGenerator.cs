using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
    public static HashSet<Vector2> floorPositions = new HashSet<Vector2>();

    protected override void RunProceduralGeneration()
    {
        floorPositions.Clear();
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {

        HashSet<Vector2> potentialRoomPositions = new HashSet<Vector2>(); // 방 만들기 위한

        CreateCorridors(floorPositions, potentialRoomPositions); // potentialRoomPositions에 분기점 마다 시작지점을 넣어줌

        HashSet<Vector2> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }
    // 특정된 막다른길 위치에 RandomWalk를 이용해 방을 생성하고 roomPositions에 추가해줌
    private void CreateRoomsAtDeadEnd(List<Vector2> deadEnds, HashSet<Vector2> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameterts, position);
                roomFloors.UnionWith(room);
            }
        }
    }
    // 막다른길 특정
    private List<Vector2> FindAllDeadEnds(HashSet<Vector2> floorPositions)
    {
        List<Vector2> deadEnds = new List<Vector2>();

        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;

            }
            if (neighboursCount == 1) // 막다른길 특정 : position기준으로 4방향을 검사 했을 때 count가 1이면 막다른 길
                deadEnds.Add(position);
        }
        return deadEnds;
    }
    // 특정한 시작점 위치에 방을 생성
    // roomPercent : 방 생성 비율
    private HashSet<Vector2> CreateRooms(HashSet<Vector2> potentialRoomPositions)
    {
        HashSet<Vector2> roomPositions = new HashSet<Vector2>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        HashSet<Vector2> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid())
                                                            .Take(roomToCreateCount).ToHashSet();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameterts, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    // 길의 시작점을 potentialRoomPositions에 넣어줌. 방생성을 위해
    // corridorCount만큼 랜덤방향으로 일직선 길을 생성함
    private void CreateCorridors(HashSet<Vector2> floorPositions, HashSet<Vector2> potentialRoomPositions)
    {
        var currentPosition = startPos;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProveduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
