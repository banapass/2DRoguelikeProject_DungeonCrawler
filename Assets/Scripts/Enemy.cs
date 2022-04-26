using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy")]
    [SerializeField] LayerMask checkMask;
    public override void Attack(Character target)
    {
        target.TakeDamage(Atk);
    }
    // 이동 및 공격
    void Ai()
    {
        // 범위 내 플레이어 체크
        if (Physics2D.OverlapBox(transform.position, new Vector2(7, 7), 120f, checkMask))
        { 
            if(isMyTurn)
            {
                // 자기 턴일 떄 
                // FindPath();
            }
        }
    }
    // A* 알고리즘
    void FindPath()
    {
        // 검색 대상
        HashSet<Vector2> targetNode = CorridorFirstDungeonGenerator.floorPositions;
        // 검색을 마친 노드 리스트
        List<Vector2> closedNode = new List<Vector2>();
        // 최단거리 저장 리스트
        List<Vector2> finalPath = new List<Vector2>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(7, 7));
    }


}
