using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    [SerializeField] protected int atk;
    [SerializeField] protected int attackRange;
    [SerializeField] protected int moveRange;
    [SerializeField] protected int def;
    [SerializeField] const int CONST_DEF = 100;
    [SerializeField] protected int exp;
    [SerializeField] protected bool isPossibleAttack = false;
    [SerializeField] protected bool isMyTurn = false;


    protected virtual int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            if (currentHp < 0)
            {

            }
            else if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }
    }
    abstract protected void Attack();
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
    }
    //protected void SearchMove(int moveRange)
    //{
    //    HashSet<Vector3> path = new HashSet<Vector3>();
    //    Vector3[] dir = { Vector3.up, Vector3.down, 
    //                      Vector3.left, Vector3.right };

    //    Queue<Vector3> q = new Queue<Vector3>();
    //    q.Enqueue(transform.position);

    //    for (int i = 0; i < moveRange; i++)
    //    {

    //        Vector3 curPos = q.Peek();
    //        q.Dequeue();
    //        for (int j = 0; j < dir.Length; j++)
    //        {
    //            Vector3 nextPos = curPos + dir[j];
    //            q.Enqueue(nextPos);
    //            path.Add(nextPos);
    //        }
    //    }
    // foreach(var positions in path)
    //    {
    //        Debug.Log(positions);
    //    }


    //}
}
