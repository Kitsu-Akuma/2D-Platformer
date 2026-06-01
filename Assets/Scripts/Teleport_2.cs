using UnityEngine;

public class TeleportToPoint : MonoBehaviour
{
    [Header("Empty GameObject where player will teleport")]
    public Transform teleportTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
        }
    }
}