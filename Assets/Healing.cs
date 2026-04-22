using UnityEngine;

public class Healing : MonoBehaviour
{
    public float healing = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Enter");
        //Destroy(collision.gameObject);
        collision.GetComponent<Health>().AddHealth(healing);
        Destroy(gameObject);
    }
}