using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] List<Item> inventroy;
    [SerializeField] int levelUpExp;


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
}
