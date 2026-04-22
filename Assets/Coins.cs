using UnityEngine;

public class Coins : MonoBehaviour
{
    public float coinValue = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Money money = collision.GetComponent<Money>();

        if (money != null)
        {
            money.AddMoney(coinValue);
            Destroy(gameObject);
        }
    }
}