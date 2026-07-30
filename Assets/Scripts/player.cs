using System;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb ; 
    
    private float xInput;
    [SerializeField]private float movementSpeed= 3.5f;
    [SerializeField]private float jumpForce= 8;

    
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

     xInput = Input.GetAxisRaw("Horizontal");

     rb.linearVelocity=new Vector2(xInput*movementSpeed,rb.linearVelocityY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity=new Vector2(rb.linearVelocityX,jumpForce);
        }
    }
}
