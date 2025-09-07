using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepBossController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float jumpCooldown = 2f;
    public LayerMask groundLayer;
    
    public int maxHealth = 5;
    public int currentHealth;
    
    public GameObject woolProjectilePrefab;
    public GameObject platformWoolPrefab;
    public Transform[] shootPoints; 
    public float shootCooldown = 3f;
    
    private Rigidbody2D rb;
    private float lastJumpTime;
    private float lastShootTime;
    private bool isGrounded;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        CheckGrounded();
        
        if (Time.time > lastJumpTime + jumpCooldown && isGrounded)
        {
            JumpTowardsPlayer();
        }
        
        if (Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
        }
    }
    
    void CheckGrounded()
    {
        var col = GetComponent<Collider2D>();
        float dist = col ? col.bounds.extents.y + 0.1f : 1.1f;
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, dist, groundLayer);
    }
    
    void JumpTowardsPlayer()
    {
        lastJumpTime = Time.time;
        
        Vector2 dir = (player.position - transform.position).normalized;
        
        Vector2 v = rb.velocity;
        v.y = 0f;
        rb.velocity = v;
        
        rb.velocity = new Vector2(dir.x * moveSpeed, jumpForce);
    }
    
    void Shoot()
    {
        lastShootTime = Time.time;
        
        if (shootPoints == null || shootPoints.Length == 0) 
        {
            return;
        }
        
        // Random chance: 70% wool projectile, 30% platform wool
        if (Random.Range(0f, 1f) <= 0.7f)
        {
            ShootWoolProjectile();
        }
        else
        {
            ShootPlatformWool();
        }
    }
    
    void ShootWoolProjectile()
    {
        if (woolProjectilePrefab == null) return;
        
        Transform shootPoint = shootPoints[Random.Range(0, shootPoints.Length)];
        GameObject projectile = Instantiate(woolProjectilePrefab, shootPoint.position, Quaternion.identity);
        
        WoolProjectile woolScript = projectile.GetComponent<WoolProjectile>();
        if (woolScript != null)
        {
            Vector2 direction = (player.position - shootPoint.position).normalized;
            woolScript.SetDirection(direction);
        }
    }
    
    void ShootPlatformWool()
    {
        if (platformWoolPrefab == null) return;
        
        Transform shootPoint = shootPoints[Random.Range(0, shootPoints.Length)];
        GameObject platformWool = Instantiate(platformWoolPrefab, shootPoint.position, Quaternion.identity);
        
        PlatformWool platformScript = platformWool.GetComponent<PlatformWool>();
        if (platformScript != null)
        {
            Vector2 midPoint = Vector2.Lerp(transform.position, player.position, 0.6f);
            Vector2 targetPosition = midPoint + Vector2.up * 3f;
            platformScript.SetTarget(targetPosition);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.CompareTag("ProjBaguette"))
        {
            Debug.Log("Enemy Hit: " + collision.gameObject.name);

            TakeDamage(1);
        }
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("EndScreen");
    }
}