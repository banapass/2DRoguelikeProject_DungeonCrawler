using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Enemy : Character
{
    [Header("Enemy")]
    [SerializeField] LayerMask checkMask;
    public override void Attack(Character target)
    {
        target.TakeDamage(Atk);
    }
    private void Awake()
    {

    }
    void Ai()
    {
        // 범위내 플레이어 체크
        if (Physics2D.OverlapBox(transform.position, new Vector2(7, 7), 120f, checkMask))
        {
            if (isMyTurn)
            {
                // 플레이어 추격
                // FindPath();
            }
        }
    }

    // A* 알고리즘
    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        // 캐싱
        HashSet<Vector2> floorPosition = CorridorFirstDungeonGenerator.floorPositions;
        Node startNode = CreateNode(startPos);
        Node targetNode = CreateNode(targetPos);

        // 방문할 노드(List로 변환)
        List<Node> openNode = new List<Node>();
        // 방문한 노드
        HashSet<Node> closedNode = new HashSet<Node>();
        // 최단거리
        List<Node> finalPath = new List<Node>();
        openNode.Add(startNode);
        while (openNode.Count > 0)
        {
            Node currentNode = openNode[0];
            for (int i = 1; i < openNode.Count; i++)
            {
                if (openNode[i].fCost <= currentNode.fCost && openNode[i].hCost < currentNode.hCost)
                {
                    currentNode = openNode[i];
                }
            }
            openNode.Remove(currentNode);
            closedNode.Add(currentNode);
            if (currentNode == )

        }

    }
    // 노드 생성
    Node CreateNode(Vector2 targetPos)
    {
        Node node = new Node();
        node.position = targetPos;
        return node;
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs((int)nodeA.position.x - (int)nodeB.position.x);
        int distanceY = Mathf.Abs((int)nodeA.position.y - (int)nodeB.position.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    // 이웃노드 검사 (상하좌우)
    List<Vector2> GetNeighbours(Vector2 pos)
    {
        HashSet<Vector2> rooms = CorridorFirstDungeonGenerator.floorPositions;
        List<Vector2> neighbours = new List<Vector2>();

        // 상하좌우 검사
        for (int i = 0; i < Direction2D.cardinalDirectionList.Count; i++)
        {
            var currentPos = pos + Direction2D.cardinalDirectionList[i];

            // 이동가능 체크
            if (rooms.Contains(currentPos))
            {
                neighbours.Add(currentPos);
            }
        }
        return neighbours;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(7, 7));
    }


}
