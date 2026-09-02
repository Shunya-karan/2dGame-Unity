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
    [SerializeField] private float damageFeedbackDuration=0.1f;
    private  Coroutine damageFeedbackCorutine;


    // public Collider2D[] enemyColiders;
    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    

    protected bool canMove = true;
    protected int facingDir = 1;
    [SerializeField] protected bool facingRight = true;


    protected virtual void Awake()
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
        Destroy(gameObject,3);
    }

    public virtual void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
    }
   

    protected virtual void HandleAttack()
    {

    }

    protected virtual void HandleAnimations()
    {
       
    }

    //Moving Player Horizontally
    protected virtual void HandleMovement()
    {       

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
    public virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
        if(attackPoint!=null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
