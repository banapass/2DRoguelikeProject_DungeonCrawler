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
    // �̵� �� ����
    void Ai()
    {
        // ���� �� �÷��̾� üũ
        if (Physics2D.OverlapBox(transform.position, new Vector2(7, 7), 120f, checkMask))
        { 
            if(isMyTurn)
            {
                // �ڱ� ���� �� 
                // FindPath();
            }
        }
    }
    // A* �˰���
    void FindPath()
    {
        // �˻� ���
        HashSet<Vector2> targetNode = CorridorFirstDungeonGenerator.floorPositions;
        // �˻��� ��ģ ��� ����Ʈ
        List<Vector2> closedNode = new List<Vector2>();
        // �ִܰŸ� ���� ����Ʈ
        List<Vector2> finalPath = new List<Vector2>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(7, 7));
    }


}
