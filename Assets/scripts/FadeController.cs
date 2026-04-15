using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Image image;

   void Awake()
{
    image = GetComponent<Image>();
    image.raycastTarget = false; 
}

    void Start()
    {
        // Fade IN when scene starts
        StartCoroutine(FadeIn());
    }

   public IEnumerator FadeIn()
{
    float t = 0f;

    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        float alpha = 1f - (t / fadeDuration);
        image.color = new Color(1f, 1f, 1f, alpha);
        yield return null;
    }

    image.color = new Color(1f, 1f, 1f, 0f);

    image.raycastTarget = false; 
}

    public IEnumerator FadeOut()
{
    image.raycastTarget = true; 

    float t = 0f;

    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        float alpha = t / fadeDuration;
        image.color = new Color(1f, 1f, 1f, alpha);
        yield return null;
    }

    image.color = new Color(1f, 1f, 1f, 1f);
}
}