using System;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb ; 
    private Animator anim;
    
    private float xInput;
    [SerializeField]private float movementSpeed= 3.5f;
    [SerializeField]private float jumpForce= 8;
    private bool isJumping;

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

//Moving Player Horizontally
    private void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocityY);
    }
   
    private void Jump(){
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    private void HandleInput(){
         if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
            isJumping=true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            Jump();
            isJumping=true;
        }
    }
    
    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity",rb.linearVelocityX);
        anim.SetFloat("yVelocity",rb.linearVelocityY);
        anim.SetBool("isGrounded",GameObject.FindGameObjectWithTag("Ground"));
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            isJumping=false;

    }
}
