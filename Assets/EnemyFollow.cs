using UnityEngine;

public class EnemyFollow : MonoBehaviour 
{
    private Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float stoppingDistance = 1f;  // How close before it stops following
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
        // Setup for floating enemy that doesn't fall
        rb.gravityScale = 0f;  // No gravity
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // Don't spin around
    }
    
    void FixedUpdate()  // Use FixedUpdate for physics-based movement
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            
            // Only move if not too close to player
            if (distance > stoppingDistance)
            {
                // Calculate direction to player
                Vector2 direction = (target.position - transform.position).normalized;
                
                // Move using velocity (works with collisions!)
                rb.velocity = direction * speed;
            }
            else
            {
                // Stop moving when close to player
                rb.velocity = Vector2.zero;
            }
        }
    }
}