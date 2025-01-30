using UnityEngine;

public class Movement : MonoBehaviour
{
    public float inputHorizontal;
    public float speed;
    public float jumpForce;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private BoxCollider2D playerCollider;

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
        }
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
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHorizontal*speed, rb.velocity.y);
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }
}
