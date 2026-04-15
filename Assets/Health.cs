using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxhealth = 10;
    private float health = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddHealing(float healing)
    {
        health += healing;
        Debug.Log(health);
    }
}
