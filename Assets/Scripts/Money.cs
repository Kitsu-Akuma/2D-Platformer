using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public float currentMoney;

    public Health health;

    public delegate void OnMoneyChangeHandler(float newMoney, float amountChanged);
    public event OnMoneyChangeHandler OnMoneyChanged;

    public delegate void OnMoneyInitializedHandler(float newMoney);
    public event OnMoneyInitializedHandler OnMoneyInitialized;

    private void Start()
    {
        currentMoney = 0;
        OnMoneyInitialized?.Invoke(currentMoney);
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney, amount);

        TryConvertMoneyToHP();
    }

    void TryConvertMoneyToHP()
    {
        int coinsToConvert = Mathf.FloorToInt(currentMoney / 10) * 10;

        if (coinsToConvert > 0)
        {
            currentMoney -= coinsToConvert;
            OnMoneyChanged?.Invoke(currentMoney, -coinsToConvert);

            health.AddHealth(coinsToConvert);
        }
    }
}