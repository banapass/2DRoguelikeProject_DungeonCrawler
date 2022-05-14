using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Profiling;

public class Enemy : Character
{
    [Header("Enemy")]
    [SerializeField] LayerMask checkMask;
    [SerializeField] Transform enemyPos;
    [SerializeField] List<Node> visualizeNode = new List<Node>();
    List<Node> finalPath = new List<Node>();
    List<Node> searchNodeList = new List<Node>();
    HashSet<Vector2> floorPosition = new HashSet<Vector2>();

    private void Awake()
    {
        floorPosition = CorridorFirstDungeonGenerator.floorPositions;
        enemyPos = GameObject.FindObjectOfType<Player>().transform;
    }
    public override void Attack(Character target)
    {
        target.TakeDamage(Atk);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            finalPath.Clear();
            searchNodeList.Clear();
            FindPath(transform.position, enemyPos.position);
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {

        Node node = new Node();
        for (int i = 0; i < searchNodeList.Count; i++)
        {

            if ((Vector2)enemyPos.position == searchNodeList[i].position)
            {
                node = searchNodeList[i];
                break;
            }
        }

        while (true)
        {

            if (node.position == (Vector2)transform.position)
                break;

            finalPath.Add(node);
            node = node.parent;
        }
        finalPath.Reverse();
        //transform.position = searchNodeList[0].position;
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

        Node startNode = CreateNode(startPos);
        Node targetNode = CreateNode(targetPos);

        // 방문할 노드(List로 변환)
        List<Node> openNode = new List<Node>();
        // 방문한 노드
        HashSet<Node> closedNode = new HashSet<Node>();
        // 최단거리


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

            if (currentNode.position == targetNode.position)
            {
                //RetracePath(startNode, targetNode);
                break;
            }

            Profiler.BeginSample("TestLine");
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (closedNode.Contains(neighbour))
                    continue;

                int neighbourCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (neighbourCost < neighbour.gCost || !openNode.Contains(neighbour))
                {
                    neighbour.gCost = neighbourCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;


                    openNode.Add(neighbour);
                    searchNodeList.Add(neighbour);

                }
            }
            Profiler.EndSample();

        }

    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode.position != startNode.position)
        {

            path.Add(currentNode);
            if (currentNode.parent == null)
                break;
            currentNode = currentNode.parent;
        }
        path.Reverse();
        visualizeNode = path;
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
    List<Node> GetNeighbours(Node node)
    {

        List<Node> neighbours = new List<Node>();

        // 상하좌우 검사
        for (int i = 0; i < Direction2D.cardinalDirectionList.Count; i++)
        {
            Node currentNode = new Node();
            currentNode.position = node.position + Direction2D.cardinalDirectionList[i];

            // 이동가능 체크
            if (floorPosition.Contains(currentNode.position))
            {
                neighbours.Add(currentNode);
            }
        }

        return neighbours;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position, new Vector2(7, 7));

        // for (int x = -5; x <= 5; x++)
        // {
        //     for (int y = -5; y <= 5; y++)
        //     {
        //         Gizmos.DrawWireCube(new Vector2(x, y), new Vector3(1, 1, 1));
        //     }
        // }
        for (int i = 0; i < searchNodeList.Count; i++)
        {
            Gizmos.DrawWireCube(searchNodeList[i].position, new Vector3(1, 1, 1));
        }
    }


}
