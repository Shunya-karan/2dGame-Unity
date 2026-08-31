using UnityEngine;

public class Enemy : Entity
{
    private bool playerDetected;
    protected override void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimations();
        HandleFlipping();
        HandleAttack();
    }
    protected override void HandleMovement()
    {
         if (playerDetected)
        {
            // Stop enemy when player is in attack range
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            return;
        }
        
        if (canMove)
            rb.linearVelocity= new Vector2(facingDir*movementSpeed,rb.linearVelocityY);
        else
            rb.linearVelocity = new Vector2(0,rb.linearVelocityY);
    }

     protected override void HandleAttack()
    {
        if (playerDetected)
        {
            canMove = false;
            anim.SetTrigger("attack");
        }
        else
        {
            canMove = true;
        }
    }

    private  void HandleCollision()
    {
        playerDetected = Physics2D.OverlapCircle(
            attackPoint.position,attackRadius,whatIsTarget
        );
    }
}

