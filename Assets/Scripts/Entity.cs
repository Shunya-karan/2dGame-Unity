using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D collider;

    protected SpriteRenderer sr;
    [Header("Health")]
    [SerializeField] private int maxhealth = 1;
    [SerializeField] private int currentHealth = 1;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration;

    private  Coroutine damageFeedbackCorutine;



    // public Collider2D[] enemyColiders;
    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected float movementSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;
    private bool isJumping;
    protected bool canMove = true;
    private bool canJump = true;

    protected int facingDir = 1;
    [SerializeField] protected bool facingRight = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxhealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlipping();
    }

    public void Attack()
{
    Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(
        attackPoint.position,
        attackRadius,
        whatIsTarget
    );

    foreach (Collider2D enemy in enemyColliders)
    {
        Entity entityTarget = enemy.GetComponent<Entity>();

        if (entityTarget != null && entityTarget != this)
        {
            entityTarget.TakeDamage();
        }
    }
}

    public void TakeDamage()
    {
        PlayDamageFeedBack();

        if (currentHealth <= 0)
            Die();
    }

    private void PlayDamageFeedBack()
    {
        currentHealth = currentHealth - 1;
        if (damageFeedbackCorutine != null)
            StopCoroutine(DamageFeedbackCoroutin());

        StartCoroutine(DamageFeedbackCoroutin());
    }

    private IEnumerator DamageFeedbackCoroutin()
    {
        Material originalMaterial = sr.material;
        sr.material = damageMaterial;

        yield return new WaitForSeconds(damageFeedbackDuration);
        sr.material = originalMaterial;
    }
    protected virtual void Die()
    {
        anim.enabled = false;
        collider.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 15);
    }

    public void EnableMovementAndJump(bool enable)
    {
        canJump = enable;
        canMove = enable;
    }
    protected void HandleInput()
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

    protected virtual void HandleAttack()
    {
        if (!isJumping)
        {
            anim.SetTrigger("attack");
        }

    }

    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));
        anim.SetFloat("yVelocity", rb.linearVelocityY);
        anim.SetBool("isJumping", isJumping);
    }

    //Moving Player Horizontally
    protected virtual void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocityY);

    }

    private void Jump()
    {
        if (!isJumping && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        else
            rb.linearVelocity = new Vector2(0, jumpForce);
    }
    protected virtual void HandleFlipping()
    {
        if (rb.linearVelocityX > 0 && facingRight == false)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && facingRight == true)
        {
            Flip();
        }

    }
    protected virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isJumping = false;

    }

    private void OnDrawGizmos()
    {
        if(attackPoint!=null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
