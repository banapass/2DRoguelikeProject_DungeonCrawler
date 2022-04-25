using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProveduralGenerationAlgorithms
{
    // 시작 지점부터 랜덤으로 방향을 뽑아 HashSet에 저장하고 리턴함
    public static HashSet<Vector2> SimpleRandomWalk(Vector2 startPos, int walkLenth)
    {
        HashSet<Vector2> path = new HashSet<Vector2>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i < walkLenth; i++)
        {
            var newPos = previousPos + Direction2D.GetRandomCardinalDireaction();
            path.Add(newPos);
            previousPos = newPos;
        }
        return path;
    }
    // 랜덤으로 방향을 정해서 일직선 길을 만듬
    public static List<Vector2> RandomWalkCorridor(Vector2 startPositions, int corridorLength)
    {
        List<Vector2> corridor = new List<Vector2>();
        var diration = Direction2D.GetRandomCardinalDireaction();
        var currentPosition = startPositions;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += diration;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<Bounds> BinarySpacePartitioning(Bounds spaceToSplit, int minWidth, int minHeight)
    {
        Queue<Bounds> roomsQueue = new Queue<Bounds>();
        List<Bounds> roomList = new List<Bounds>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }

                }
                else
                {

                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }
        return roomList;
    }

    private static void SplitVertically(int minWidth, Queue<Bounds> roomsQueue, Bounds room)
    {
        var xSplit = Random.Range(1, room.size.x);
        Bounds room1 = new Bounds(room.min, new Vector3(xSplit, room.size.y, room.size.z));
        Bounds room2 = new Bounds(new Vector3(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<Bounds> roomsQueue, Bounds room)
    {
        var ySplit = Random.Range(1, room.size.y);
        Bounds room1 = new Bounds(room.min, new Vector3(room.size.x, ySplit, room.size.z));
        Bounds room2 = new Bounds(new Vector3(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2> cardinalDirectionList = new List<Vector2>
    {
        new Vector2(0,1),//Up
        new Vector2(1,0),//Right
        new Vector2(0,-1), // Down
        new Vector2(-1,0) // Left
    };
    public static Vector2 GetRandomCardinalDireaction()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}