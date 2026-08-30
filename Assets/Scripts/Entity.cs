using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb ; 
    protected Animator anim;
    
    // public Collider2D[] enemyColiders;
    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField]protected float movementSpeed;
    [SerializeField]private float jumpForce;

    private float xInput;
    private bool isJumping;
    protected bool canMove=true;
    private bool canJump=true;

    protected int facingDir = 1;
    [SerializeField] private bool facingRight=true;
    

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlipping();
    }

    public void DamageTarget()
    {
       Collider2D[] enemyColiders = Physics2D.OverlapCircleAll(attackPoint.position,attackRadius, whatIsTarget);

       foreach (Collider2D enemy in enemyColiders)
       {
        Entity entityTarget = enemy.GetComponent<Entity>();
        entityTarget.DamageTarget();

    }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canJump=enable;
        canMove=enable;
    }
    protected void HandleInput(){
         xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            Jump();
            isJumping=true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryToAttack();
        }
    }
    
    protected virtual void TryToAttack()
    {
        if (!isJumping)
        {
            anim.SetTrigger("attack");
        }
        
    }

    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity",Math.Abs(rb.linearVelocityX));
        anim.SetFloat("yVelocity",rb.linearVelocityY);
        anim.SetBool("isJumping",isJumping);
    }

    //Moving Player Horizontally
    protected virtual void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocityY);
        
    }
   
    private void Jump(){
        if(!isJumping && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        else
             rb.linearVelocity = new Vector2(0, jumpForce);
    }
    protected void HandleFlipping()
    {
        if(rb.linearVelocityX>0 && facingRight == false)
        {
            Flip();
        }
        else if (rb.linearVelocityX<0 && facingRight == true)
        {
            Flip();
        }
    
    }
    private void Flip()
    {
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
        facingDir = facingDir*-1;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            isJumping=false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
