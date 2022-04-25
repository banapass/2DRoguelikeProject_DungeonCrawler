using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField][Range(0, 10)] private int offset = 1;
    [SerializeField] private bool randomWalkRooms = false;


    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProveduralGenerationAlgorithms.BinarySpacePartitioning(new Bounds((Vector3)startPos,
                       new Vector3(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2> floor = new HashSet<Vector2>();
        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }


        List<Vector2> roomCenters = new List<Vector2>();

        foreach (var room in roomsList)
        {
            roomCenters.Add(((Vector2)room.center));
        }

        HashSet<Vector2> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2> CreateRoomsRandomly(List<Bounds> roomsList)
    {
        HashSet<Vector2> floor = new HashSet<Vector2>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameterts, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.min.x + offset) && position.x <= (roomBounds.max.x - offset) &&
                    position.y >= (roomBounds.min.y - offset) && position.y <= (roomBounds.max.y - offset))
                {

                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2> ConnectRooms(List<Vector2> roomCenters)
    {
        HashSet<Vector2> corridors = new HashSet<Vector2>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);


        while (roomCenters.Count > 0)
        {
            Vector2 colsest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(colsest);
            HashSet<Vector2> newCorridor = CreateCorridor(currentRoomCenter, colsest);
            currentRoomCenter = colsest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2> CreateCorridor(Vector2 currentRoomCenter, Vector2 destination)
    {
        HashSet<Vector2> corridor = new HashSet<Vector2>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2 FindClosestPointTo(Vector2 currentRoomCenter, List<Vector2> roomCenters)
    {
        Vector2 closest = Vector2.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2> CreateSimpleRooms(List<Bounds> roomsList)
    {
        HashSet<Vector2> floor = new HashSet<Vector2>();

        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2 position = (Vector2)room.min + new Vector2(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
