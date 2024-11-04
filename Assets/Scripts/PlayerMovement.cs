using System.Transactions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] bool onGround = false;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    [SerializeField] private LayerMask groundLayer;


    private Animator playerAnimator;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(3.610736f, 3.610736f, 3.610736f);
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-3.610736f, 3.610736f, 3.610736f);
        }

        playerAnimator.SetBool("walk", horizontalInput != 0);

        onGround = CheckIsGrounded();
        playerAnimator.SetBool("isGrounded", onGround);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce);
            playerAnimator.SetBool("isGrounded", onGround);
        }
    }

   

    private bool CheckIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && CheckIsGrounded();
    }
}

