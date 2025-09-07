using UnityEngine;

public class ShootBaguette : MonoBehaviour
{
    public float delay = 5f;
    public float speed = 12f;

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void OnEnable() => Destroy(gameObject, delay);

    public void SetDirection(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        rb.linearVelocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var boss = collision.GetComponent<SheepBossController>();
            if (boss != null) boss.TakeDamage(1);
            else Destroy(collision.gameObject);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
