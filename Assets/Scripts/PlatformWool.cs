using System.Collections;
using UnityEngine;

public class PlatformWool : MonoBehaviour
{
    public float projectileSpeed = 6f;
    public float arcHeight = 3f;

    public float platformLifetime = 10f;
    public Vector2 colliderSizeMultiplier = new Vector2(1f, 1f);

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isFlying = true;
    private bool isPlatform = false;
    private float journeyLength;

    // Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();


        if (col != null)
            col.isTrigger = true;

        if (rb != null)
            rb.gravityScale = 0;
    }

    public void SetTarget(Vector2 target)
    {
        startPosition = transform.position;
        targetPosition = target;
        journeyLength = Vector2.Distance(startPosition, targetPosition);

        float flightTime = journeyLength / projectileSpeed;


        StartCoroutine(FlyInArc(flightTime));
    }

    IEnumerator FlyInArc(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            Vector2 currentPosition = Vector2.Lerp(startPosition, targetPosition, progress);
            float arcProgress = 4 * progress * (1 - progress);
            currentPosition.y += arcHeight * arcProgress;

            transform.position = currentPosition;

            yield return null;
        }

        BecomePlatform();
    }

    void BecomePlatform()
    {
        isFlying = false;
        isPlatform = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col != null && sr != null)
        {
            col.isTrigger = false;

            Vector2 spriteSize = sr.bounds.size;
            col.size = new Vector2(
                spriteSize.x * colliderSizeMultiplier.x,
                spriteSize.y * colliderSizeMultiplier.y
            );
            col.offset = Vector2.zero;
        }

        if (sr != null)
            sr.color = Color.white;

        gameObject.layer = LayerMask.NameToLayer("Ground");
        gameObject.tag = "Platform";

        Destroy(gameObject, platformLifetime);

        StartCoroutine(PlatformSpawnEffect());
    }

    IEnumerator PlatformSpawnEffect()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.5f;

        float time = 0f;
        while (time < 0.3f)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(0.5f, 1f, time / 0.3f);
            transform.localScale = originalScale * scale;
            yield return null;
        }

        transform.localScale = originalScale;

        if (col != null && sr != null)
        {
            Vector2 spriteSize = sr.sprite.bounds.size;
            col.size = new Vector2(
                spriteSize.x * colliderSizeMultiplier.x,
                spriteSize.y * colliderSizeMultiplier.y
            );
            col.offset = Vector2.zero;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFlying)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Player"))
            {
                StopAllCoroutines();
                BecomePlatform();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isPlatform && collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
    