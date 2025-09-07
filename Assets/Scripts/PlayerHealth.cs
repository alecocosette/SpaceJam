using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public Slider healthBar; 
    public Text healthText;
    
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;
    

    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthUI();
    }
    
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                
        StartCoroutine(DamageEffect());
        
        UpdateHealthUI();
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityPeriod());
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        UpdateHealthUI();
    }
    
    IEnumerator DamageEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }
    
    IEnumerator InvincibilityPeriod()
    {
        isInvincible = true;
        
        if (spriteRenderer != null)
        {
            float flickerTime = 0.1f;
            float totalTime = 0f;
            
            while (totalTime < invincibilityDuration)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); 
                yield return new WaitForSeconds(flickerTime);
                spriteRenderer.color = Color.white; 
                yield return new WaitForSeconds(flickerTime);
                totalTime += flickerTime * 2;
            }
            
            spriteRenderer.color = Color.white; 
        }
        else
        {
            yield return new WaitForSeconds(invincibilityDuration);
        }
        
        isInvincible = false;
    }
    
    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
        
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
    
    void Die()
    {
        
        

        gameObject.SetActive(false);
    }
    
    public bool CanTakeDamage()
    {
        return !isInvincible;
    }
}