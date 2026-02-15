using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadeImage;
    public string eventName = "FadeOut";
    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
    }

    IEnumerator FadeOutCoroutine(float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        fadeImage.enabled = false; // Disabilita l'immagine dopo il fade out
    }

    public void ProcessEvent(string eventName)
    {
        if (eventName == this.eventName)
        {
            StartCoroutine(FadeOutCoroutine(2f)); // Durata di 2 secondi per il fade out

        }
    }
}
