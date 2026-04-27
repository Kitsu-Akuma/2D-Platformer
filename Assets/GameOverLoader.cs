using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public Health playerHealth;
    public string gameOverSceneName = "GameOver";

    void Update()
    {
        if (playerHealth == null) return;

        if (playerHealth.GetCurrentHealth() <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}