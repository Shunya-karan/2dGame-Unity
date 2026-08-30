using UnityEngine;

public class Enemy : Entity
{
    protected override void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlipping();
    }
    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity= new Vector2(facingDir*movementSpeed,rb.linearVelocityY);
        else
            rb.linearVelocity = new Vector2(0,rb.linearVelocityY);
    }
}
