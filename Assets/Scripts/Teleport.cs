using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    // Możesz ustawić numer sceny lub nazwę sceny, która ma się włączyć
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // zakładając że gracz ma tag "Player"
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}