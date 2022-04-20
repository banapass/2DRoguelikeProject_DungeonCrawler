using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] List<Item> inventroy;
    [SerializeField] int levelUpExp;
    [SerializeField] GameObject test;

    int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            if (exp >= levelUpExp)
            {
                LevelUp();
            }

        }
    }
    bool IsMyTurn
    {
        get { return isMyTurn; }
        set
        {
            isMyTurn = value;
            if (isMyTurn)
            {
                StartCoroutine(Move());
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsMyTurn = true;
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SearchMove(2);
        }
    }
    IEnumerator Move()
    {

        while (isMyTurn)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position += Vector3.up;
                isMyTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position += Vector3.down;
                isMyTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += Vector3.left;
                isMyTurn = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right;
                isMyTurn = false;
            }
            yield return null;
        }
    }
    private void LevelUp()
    {

    }
    public void Equip()
    {

    }
    #region 캡슐화를 위한 함수
    public void Healing(int heal)
    {
        currentHp += heal;
    }
    public void GetExp(int exp)
    {
        this.exp += exp;
    }
    #endregion

    protected override void Attack()
    {
        throw new NotImplementedException();
    }
    protected void SearchMove(int moveRange)
    {
        HashSet<Vector3> path = new HashSet<Vector3>();
        Vector3[] dir = { Vector3.up, Vector3.down,
                          Vector3.left, Vector3.right };

        Queue<Vector3> q = new Queue<Vector3>();
        q.Enqueue(transform.position);


        for(int i = 0; i < moveRange; i++)
        {
            FindPath(q);
        }

    }
    void FindPath(Queue<Vector3> q)
    {
        HashSet<Vector3> path = new HashSet<Vector3>();
        Vector3[] dir = { Vector3.up, Vector3.down,
                          Vector3.left, Vector3.right };

        int qCount = q.Count;
        for(int i = 0; i < qCount; i++)
        {
            Vector3 curPos = q.Peek();
            q.Dequeue();
            for (int j = 0; j < dir.Length; j++)
            {

                Vector3 nextPos = curPos + dir[j];
                q.Enqueue(nextPos);
                path.Add(nextPos);
            }

        }
        CreatePath(path);
    }
    void CreatePath(HashSet<Vector3> positions)
    {
        foreach(var pos in positions)
        {
            
            GameObject temp = Instantiate(test);
            temp.transform.position = pos;
        }
    }

}
