using TMPro;
using UnityEngine;

public class UI_MoneyDisplay : MonoBehaviour
{
    public Money MoneyComponent;
    public TextMeshProUGUI textComponent;

    private void Awake()
    {
        MoneyComponent.OnMoneyInitialized += OnMoneyInitialized;
        MoneyComponent.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnMoneyInitialized(float newMoney)
    {
        textComponent.text = newMoney.ToString();
    }

    private void OnMoneyChanged(float newMoney, float amountChanged)
    {
        textComponent.text = newMoney.ToString();
    }
}