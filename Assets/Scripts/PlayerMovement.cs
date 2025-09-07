using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpingPower = 16f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public GameObject baguettePrefab;  
    public Transform LaunchOffset; 
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // --- Movement ---
        horizontal = Keyboard.current.aKey.isPressed ? -1f :
                     Keyboard.current.dKey.isPressed ? 1f : 0f;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        
        Flip();

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ShootBaguette();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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

    private void ShootBaguette()
    {
        if (baguettePrefab != null && LaunchOffset != null)
        {
            GameObject baguette = Instantiate(baguettePrefab, LaunchOffset.position, Quaternion.identity);

            ShootBaguette baguetteScript = baguette.GetComponent<ShootBaguette>();
            if (baguetteScript != null)
            {
                Vector3 shootDirection = isFacingRight ? Vector3.right : Vector3.left;
                baguetteScript.SetDirection(shootDirection);
            }
        }
    }
}