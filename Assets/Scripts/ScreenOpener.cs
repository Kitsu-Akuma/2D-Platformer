using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class SceneOpener : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float waitOnBlack = 0.3f;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void OpenScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }
    IEnumerator FadeAndLoad(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade");
            yield break;
        }
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0, 1, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(waitOnBlack);
        yield return SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float t = 0f;
        fadeImage.color = new Color(0, 0, 0, 1);
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}