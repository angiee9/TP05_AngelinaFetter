using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    [SerializeField] private float JumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float input;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] bool onGround = false;
    [SerializeField] bool walk = false;


    private Animator playerAnimator;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if(input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(input > 0)
        {
            spriteRenderer.flipX= false;
        }

        playerAnimator.SetBool("walk", walk);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            walk = true;
        }

        playerAnimator.SetBool("isGrounded", onGround);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true || walk == true)
            {
                rb.AddForce(Vector2.up * JumpForce);
                onGround = false;
                walk = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (onGround == false)
            {
                onGround = true;
            }
        }
    }
}

