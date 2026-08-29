using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb ; 
    protected Animator anim;
    
    // public Collider2D[] enemyColiders;
    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField]private float movementSpeed;
    [SerializeField]private float jumpForce;

    private float xInput;
    private bool isJumping;
    private bool canMove=true;
    private bool canJump=true;
    

    [SerializeField] private bool facingRight=true;
    

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimations();
        PlayerFlipping();
    }

    public void DamageEnemies()
    {
       Collider2D[] enemyColiders = Physics2D.OverlapCircleAll(attackPoint.position,attackRadius, whatIsEnemy);

    //    foreach (Collider2D enemy in enemyColiders)
    //    {
    //     Enemy enemyScript = enemy.GetComponent<Enemy>();
    //     enemyScript.TakeDamage();

    //     String enemyName = enemyScript.getEnemyName();
    //     Debug.Log("AttAck on "+enemyName);
    //    }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canJump=enable;
        canMove=enable;
    }
    private void HandleInput(){
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
    
    private void TryToAttack()
    {
        if (!isJumping)
        {
            anim.SetTrigger("attack");
        }
        
    }
    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity",Math.Abs(rb.linearVelocityX));
        anim.SetFloat("yVelocity",rb.linearVelocityY);
        anim.SetBool("isJumping",isJumping);
    }

    //Moving Player Horizontally
    private void HandleMovement()
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
    private void PlayerFlipping()
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            isJumping=false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
