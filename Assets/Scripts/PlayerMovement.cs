using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float input;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] bool onGround = false;
    [SerializeField] bool walk = false;
    private BoxCollider2D boxCollider;
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

        playerAnimator.SetBool("walk", input != 0);

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
        return input == 0 && CheckIsGrounded();
    }
}

