using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public AudioClip jumpClip;
    private AudioSource jumpSource;
    private void Start()
    {
        jumpSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        // New Input System - WASD and Arrow keys support
        float wasd = Keyboard.current.aKey.isPressed ? -1f : Keyboard.current.dKey.isPressed ? 1f : 0f;
        float arrows = Keyboard.current.leftArrowKey.isPressed ? -1f : Keyboard.current.rightArrowKey.isPressed ? 1f : 0f;
        horizontal = wasd != 0f ? wasd : arrows;
        
        // Jump input - Space key
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            jumpSource.PlayOneShot(jumpClip);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        Flip();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }
    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
