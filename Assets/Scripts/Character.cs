using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    [SerializeField] protected int atk;
    [SerializeField] protected int attackRange;
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
}
