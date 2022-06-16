using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Profiling;

public class Enemy : Character
{
    [Header("Enemy")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] Player target;
    public Player Target
    {
        get { return target; }
    }
    [SerializeField] List<Node> visualizeNode = new List<Node>();
    List<Node> finalPath = new List<Node>();
    List<Node> searchNodeList = new List<Node>();
    HashSet<Vector2> floorPosition = new HashSet<Vector2>();

    Vector2 prevPos;



    private void Awake()
    {
        floorPosition = CorridorFirstDungeonGenerator.floorPositions;

    }
    public override void Attack(Character target)
    {
        target.TakeDamage(Atk);
    }


    // 일정 범위 내 플레이어 체크
    public void CheckArea()
    {

        Collider2D col = Physics2D.OverlapCircle(transform.position, 5, playerMask);


        if (Physics2D.OverlapCircle(transform.position, 5, playerMask))
        {

            target = col.GetComponent<Player>();
            if (!GameManager.checkEnemys.Contains(this))
            {
                GameManager.checkEnemys.Add(this);
            }

        }
        else
        {
            if (GameManager.checkEnemys.Contains(this))
            {
                GameManager.checkEnemys.Remove(this);
            }

            target = null;
        }

    }
    public void TryMove()
    {
        if (target != null)
        {
            finalPath.Clear();
            searchNodeList.Clear();
            FindPath((Vector2)target.transform.position);
            MoveToTarget();
            GameManager.IsPlayerTurn = true;
        }
    }
    // A* 알고리즘
    void FindPath(Vector2 targetPos)
    {

        Node startNode = CreateNode((Vector2)transform.position);
        Node targetNode = CreateNode(targetPos);

        // 방문할 노드
        List<Node> openNode = new List<Node>();
        // 방문한 노드
        HashSet<Node> closedNode = new HashSet<Node>();


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

            if (currentNode.position == targetPos)
            {
                RetracePath(currentNode);
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
    void RetracePath(Node endNode)
    {

        Node currentNode = endNode;

        while (currentNode.position != (Vector2)transform.position)
        {

            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
    }

    // 이동 및 공격
    void MoveToTarget()
    {

        if (EnemyCheck())
        {
            target.TakeDamage(atk);

        }
        else
        {
            prevPos = transform.position;
            transform.position = finalPath[0].position;
        }


    }
    public void ResearchPath()
    {
        transform.position = prevPos;
        FindPath(target.transform.position);
        MoveToTarget();
    }

    bool EnemyCheck()
    {
        bool check = false;
        for (int i = 0; i <= attackRange; i++)
        {
            // 공격 범위 내에 적이 있을 시
            if (Physics2D.Linecast(transform.position, finalPath[i].position, playerMask))
            {
                check = true;
                break;
            }
            // 공격 범위 내에 없을 시
            else
            {
                check = false;
            }
        }
        return check;
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
        // for (int i = 0; i < Direction2D.cardinalDirectionList.Count; i++)
        // {
        //     Node currentNode = new Node();
        //     currentNode.position = node.position + Direction2D.cardinalDirectionList[i];

        //     // 이동가능 체크
        //     if (floorPosition.Contains(currentNode.position) && !Physics2D.Linecast(currentNode.position, currentNode.position, enemyMask))
        //     {
        //         neighbours.Add(currentNode);
        //     }
        // }

        //대각선 추가 검사
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Node currentNode = new Node();
                currentNode.position = node.position + new Vector2(x, y);

                if (floorPosition.Contains(currentNode.position) && !Physics2D.Linecast(currentNode.position, currentNode.position, enemyMask))
                {
                    neighbours.Add(currentNode);
                }
            }
        }

        return neighbours;
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.GetComponent<Enemy>() != null)
    //     {
    //         GameManager.researchTarget.Add(other.GetComponent<Enemy>());
    //         GameManager.ResearchEnemys();
    //     }
    // }
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

        // 탐색한 노드 디버깅
        for (int i = 0; i < searchNodeList.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(searchNodeList[i].position, new Vector3(1, 1, 1));
        }

        // 최단거리 디버깅
        for (int i = 0; i < finalPath.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(finalPath[i].position, new Vector3(1, 1, 1));
        }

    }


}
