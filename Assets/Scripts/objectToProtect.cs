using System;
using UnityEngine;

public class objectToProtect : Entity
{
    [Header("Extra Details")]
    [SerializeField] private Transform player;
    protected override void Update()
    {
        HandleFlipping();
    }

    protected override void HandleFlipping()
    {
         if (player.transform.position.x>transform.position.x && facingRight==false)
        {
            Flip();
        }
        else if (player.transform.position.x<transform.position.x && facingRight==true)
        {
            Flip();
        }
    }
}
