using System.Collections;
using UnityEngine;
using TMPro;

public class TextAutoFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float duration = 1.5f;

    private void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        Color c = text.color;
        c.a = 0f;
        text.color = c;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            float alpha = Mathf.SmoothStep(0f, 1f, t / duration);

            c = text.color;
            c.a = alpha;
            text.color = c;

            yield return null;
        }

        c = text.color;
        c.a = 1f;
        text.color = c;
    }
}