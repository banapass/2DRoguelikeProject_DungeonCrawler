using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea : MonoBehaviour
{
    public Player target;
    [SerializeField] Enemy enemy;
    private void OnMouseDown()
    {
        if (enemy != null)
        {
            enemy.TakeDamage(target.Atk);
            target.IsMyTurn = false;
            Debug.Log("Attack");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemy = other.GetComponent<Enemy>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enemy = null;
    }
    public void DestroyArea()
    {
        Destroy(gameObject);
    }
}
