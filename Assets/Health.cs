using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    private bool invincibility;

    private SpriteRenderer spriteRenderer;

    public delegate void OnHealthChangeHandler(float newHealth, float amountChanged);
    public event OnHealthChangeHandler OnHealthChanged;

    public delegate void OnHealthInitializedHandler(float newHealth);
    public event OnHealthInitializedHandler OnHealthInitialized;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthInitialized?.Invoke(currentHealth);
    }

    public void ReceiveDamage(float amount)
    {
        if (!invincibility && currentHealth > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);

            OnHealthChanged?.Invoke(currentHealth, -amount);

            invincibility = true;
            StartCoroutine(InvincibilityFade(2f));   
            if (currentHealth <= 0)
        {
            Destroy(gameObject);
                SceneManager.LoadScene("Game Over");
        }
        }
    }

    private IEnumerator InvincibilityFade(float duration)
    {
        float timer = 0f;
        bool visible = true;

        Color originalColor = spriteRenderer.color;

        while (timer < duration)
        {
            visible = !visible;

            float alpha = visible ? 1f : 0.3f;

            spriteRenderer.color = new Color(
                originalColor.r,
                originalColor.g,
                originalColor.b,
                alpha
            );

            yield return new WaitForSeconds(0.12f);
            timer += 0.12f;
        }

        spriteRenderer.color = new Color(
            originalColor.r,
            originalColor.g,
            originalColor.b,
            1f
        );

        invincibility = false;
    }

    public void AddHealth(float amount)
    {
        float oldHealth = currentHealth;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        float realAmount = currentHealth - oldHealth;

        if (realAmount > 0)
        {
            OnHealthChanged?.Invoke(currentHealth, realAmount);
        }
     
    }

    internal int GetCurrentHealth()
    {
        throw new NotImplementedException();
        return (int)currentHealth;
    }
}