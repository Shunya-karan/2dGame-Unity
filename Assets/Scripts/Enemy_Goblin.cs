using System;
using UnityEngine;

public class Enemy_Goblin : Enemy
{
   private void Awake()
    {
        moveSpeed = 10;

    }

    protected override void Attack()
    {
        base.Attack();
        StealMoney();
    }

    [ContextMenu("Steal Gold")]

    private void StealMoney()
    {
        // Debug.Log(enemyName +"Steal Money");
    }

  
}
