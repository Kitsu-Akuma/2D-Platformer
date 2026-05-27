using System;
using TMPro;
using UnityEngine;

public class UI_HealthDisplay : MonoBehaviour
{
    public Health healthComponent;
    public TextMeshProUGUI textComponent;
    void Start()
    {
        healthComponent.OnHealthChanged += OnHealthChanged;
        healthComponent.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnHealthInitialized(float newHealth)
    {
        textComponent.text = newHealth.ToString();
    }

    private void OnHealthChanged(float newHealth, float amountChanged)
    {
        Debug.Log(newHealth + ":" + amountChanged);
        textComponent.text = newHealth.ToString();
    }
}
