using UnityEngine;

public class Movement : MonoBehaviour
{
    public float inputHorizontal;
    public float speed;
    public float jumpForce;
    public bool crouchEnabled = true;
    public bool jumpEnabled = true;
    public bool movementEnabled = true;
    public int extraJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private BoxCollider2D playerCollider;

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (jumpEnabled)
        {
            if (Input.GetButtonDown("Jump") && extraJump > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJump -= extraJump;
            }else if(Input.GetButtonDown("Jump") && extraJump == 0 && isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce*0.7f);
            }
            if (isGrounded() && extraJump == 0)
            {
                extraJump = 1;
            }
        }
        if (crouchEnabled)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                spriteTransform.localScale = new Vector2(spriteTransform.localScale.x, spriteTransform.localScale.y * 0.5f);
                playerCollider.size = new Vector2(1f, 1f);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                spriteTransform.localScale = new Vector2(spriteTransform.localScale.x, spriteTransform.localScale.y * 2f);
                playerCollider.size = new Vector2(1f, 2f);
            }
            if (isCeilinged())
            {
                Debug.Log("1");
            }
        }
    }
    private void FixedUpdate()
    {
        if (movementEnabled)
        {
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .5f, groundLayer);
    }
    private bool isCeilinged()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, .5f, groundLayer);
    }
}
