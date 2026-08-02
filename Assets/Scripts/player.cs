using System;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb ; 
    private Animator anim;
    
    private float xInput;
    [SerializeField]private float movementSpeed= 3.5f;
    [SerializeField]private float jumpForce= 8;

    
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimations();
    }



    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

         if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocityY);
    }
        
    private void HandleAnimations()
    {
        bool isMoving= rb.linearVelocityX!=0;
        anim.SetBool("isMoving",isMoving);
    }
}
