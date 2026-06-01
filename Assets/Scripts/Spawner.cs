using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Prefab pickupa")]
    public GameObject pickupPrefab;

    [Header("Losowe sprite'y pickupów")]
    public Sprite[] pickupSprites;

    [Header("Miejsca spawnu - puste node'y")]
    public Transform[] spawnPoints;

    [Header("Czy spawnować na starcie gry?")]
    public bool spawnOnStart = true;

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnPickups();
        }
    }

    public void SpawnPickups()
    {
        foreach (Transform point in spawnPoints)
        {
            SpawnOnePickup(point);
        }
    }

    private void SpawnOnePickup(Transform spawnPoint)
    {
        if (pickupPrefab == null || pickupSprites.Length == 0)
        {
            Debug.LogWarning("Brakuje prefabu pickupa albo sprite'ów.");
            return;
        }

        GameObject pickup = Instantiate(
            pickupPrefab,
            spawnPoint.position,
            Quaternion.Euler(0f, 0f, 0f)
        );

        pickup.transform.localScale = Vector3.one * 5f;
        SpriteRenderer sr = pickup.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            Sprite randomSprite = pickupSprites[Random.Range(0, pickupSprites.Length)];
            sr.sprite = randomSprite;
        }
        else
        {
            Debug.LogWarning("Pickup prefab nie ma SpriteRenderer.");
        }
    }
}