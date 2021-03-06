using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField] int maxExp;
    [SerializeField] int Level;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] MoveArea moveArea;
    [SerializeField] List<MoveArea> moveAreaList = new List<MoveArea>(); // 생성뒤 삭제용

    [SerializeField] Weapon currentWeapon;
    public Weapon CurrentWeapon
    {
        get { return currentWeapon; }
        set
        {
            if (currentWeapon != null)
            {
                atk -= currentWeapon.IncreaceAtk;
            }
            currentWeapon = value;
            atk += currentWeapon.IncreaceAtk;
        }
    }

    int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            if (exp >= maxExp)
            {
                LevelUp();
            }

        }
    }
    public bool IsMyTurn
    {
        get { return GameManager.IsPlayerTurn; }
        set
        {
            GameManager.IsPlayerTurn = value;
            if (GameManager.IsPlayerTurn)
            {
                StartCoroutine(Move());
            }
            else
            {

                for (int i = 0; i < moveAreaList.Count; i++)
                {
                    moveAreaList[i].DestroyArea();
                }
                moveAreaList.Clear();


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
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && IsMyTurn)
        {
            AttackableAreaCheck(attackRange);
        }
    }

    IEnumerator Move()
    {

        while (IsMyTurn)
        {

            if (Input.GetKeyDown(KeyCode.W) && MoveDirectionCheck(Vector3.up))
            {
                transform.position += Vector3.up;
                IsMyTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.S) && MoveDirectionCheck(Vector3.down))
            {
                transform.position += Vector3.down;
                IsMyTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.A) && MoveDirectionCheck(Vector3.left))
            {
                transform.position += Vector3.left;
                IsMyTurn = false;
            }
            if (Input.GetKeyDown(KeyCode.D) && MoveDirectionCheck(Vector3.right))
            {
                transform.position += Vector3.right;
                IsMyTurn = false;
            }
            yield return null;
        }
    }
    bool MoveDirectionCheck(Vector3 dir)
    {
        if (Physics2D.Linecast(transform.position, transform.position + dir, wallLayer))
        {
            return false;
        }
        return true;
    }
    private void LevelUp()
    {
        // 레벨업 로직 추가 필요
        exp = 0;
    }

    public void Equip(Item equip)
    {
        // 장비 장착 로직 필요
        if (equip.itemType == Item.ItemType.Weapon)
        {
            Weapon weapon = (Weapon)equip;
            CurrentWeapon = weapon;
        }


    }
    #region 캡슐화를 위한 함수
    public void Healing(Item item)
    {
        Potion potion = (Potion)item;
        CurrentHp += potion.IncreaceHp;
    }
    public void GetExp(int exp)
    {
        this.Exp += exp;
    }
    #endregion

    public override void Attack(Character target)
    {
        target.TakeDamage(atk);
    }

    private void AttackableAreaCheck(int attackRange)
    {
        HashSet<Vector3> path = new HashSet<Vector3>();
        Vector3[] dir = { Vector3.up, Vector3.down,
                          Vector3.left, Vector3.right };

        Queue<Vector3> q = new Queue<Vector3>();
        q.Enqueue(transform.position);


        for (int i = 0; i < attackRange; i++)
        {
            FindPath(q);
        }
    }
    // BFS활용 이동 범위 체크
    void FindPath(Queue<Vector3> q)
    {
        HashSet<Vector3> path = new HashSet<Vector3>();
        Vector3[] dir = { Vector3.up, Vector3.down,
                          Vector3.left, Vector3.right };

        int qCount = q.Count;
        for (int i = 0; i < qCount; i++)
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
        AttackableAreaVisualize(path);
    }

    // 특정한 위치 시각화 및 세팅
    void AttackableAreaVisualize(HashSet<Vector3> positions)
    {

        foreach (var pos in positions)
        {
            if (CorridorFirstDungeonGenerator.floorPositions.Contains(pos))
            {
                MoveArea temp = Instantiate(moveArea.gameObject).GetComponent<MoveArea>();
                moveAreaList.Add(temp);
                temp.target = this;
                temp.transform.position = pos;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PickUp>() != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && Inventory.CheckEmptySlot())
            {
                Inventory.AddItem(collision.GetComponent<PickUp>().item);
                Destroy(collision.gameObject);
            }
        }
    }

}
