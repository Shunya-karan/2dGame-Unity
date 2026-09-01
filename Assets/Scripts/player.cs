using System;
using UnityEngine;

public class player : Entity
{
    [Header("Movement Details")]
    [SerializeField] protected float movementSpeed;
    private float xInput;
    [SerializeField] private float jumpForce;
    private bool isJumping;
    private bool canJump = true;


    protected override void Update()
    {
        base.Update();
        HandleInput();
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocityY);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }

    private  void HandleInput()
    {
         xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            Jump();
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleAttack();
        }
    }

    private  void Jump()
    {
        if (!isJumping && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        else
            rb.linearVelocity = new Vector2(0, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isJumping = false;

    }

    protected override void HandleAttack()
    {
        if (!isJumping)
        {
            anim.SetTrigger("attack");
        }
    }

    protected override void HandleAnimations()
    {
        anim.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));
        anim.SetFloat("yVelocity", rb.linearVelocityY);
        anim.SetBool("isJumping", isJumping);
    }

    public override void EnableMovementAndJump(bool enable)
    {
        base.EnableMovementAndJump(enable);
        canJump = enable;
    }
}
