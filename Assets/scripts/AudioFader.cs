using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Fade Settings")]
    public float fadeInDuration = 2f;
    public float fadeOutDuration = 2f;
    [Range(0f, 1f)] public float targetVolume = 1f;

    [Header("Debug Buttons")]
    public bool fadeInOnStart = false;

    private Coroutine currentFade;

    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        audioSource.volume = 0f;
        audioSource.Play();

        float time = 0f;

        while (time < fadeInDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, time / fadeInDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}