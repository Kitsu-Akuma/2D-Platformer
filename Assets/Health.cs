using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    private bool invicibility;

    public delegate void OnHealthChangeHandler(float newHealth, float amountChanged);
    public event OnHealthChangeHandler OnHealthChanged;

    public delegate void OnHealthInitializedHandler(float newHealth);
    public event OnHealthInitializedHandler OnHealthInitialized;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthInitialized?.Invoke(currentHealth);
    }

    public void ReceiveDamage(float amount)
    {
        if (!invicibility)
        {
            currentHealth -= amount;
            OnHealthChanged?.Invoke(currentHealth, amount);
            invicibility = true;
        }
    }

    IEnumerator ResetInvicibility(float resetTime);
    {
    yield return new WaitForSeconds(resetTime);
    Debug.Log("Reset");
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, amount);
        //Debug.Log(currentHealth);
    }
}
