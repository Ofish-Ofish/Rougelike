using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    private float horizontalMove = 0f;
    [SerializeField] private float speed;

    [SerializeField] Transform groundCheck;
    public LayerMask groundLayer;

    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMult;
    private Vector2 vecGravity;
    private bool jumpPressed;

    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMult;
    private bool isJumping;
    private float jumpCounter;
    private bool jumpReleased;

    void Awake()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        jumpReleased = false;
        jumpPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            isJumping = true;
            jumpCounter = 0;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Jump"))
        {
            jumpReleased = true;
            isJumping = false;
            jumpCounter = 0;
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMove * speed, rb.linearVelocity.y);

        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }

        if (rb.linearVelocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
            }
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMult;

            if (t > 0.5f)
            {
                currentJumpM = jumpMult * (1 - t);
            }

            rb.linearVelocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (jumpReleased && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * .6f);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity -= vecGravity * fallMult * Time.deltaTime;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.2f, .1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

}
