using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FailFlash : MonoBehaviour
{
    public static FailFlash Instance;

    public Image panel;
    public float fadeInTime = 0.05f;
    public float holdTime = 0.2f;
    public float fadeOutTime = 0.1f;

    bool isPlaying;

    void Awake()
    {
        Instance = this;
    }

    public void PlayFlash(System.Action onComplete = null)
    {
        if (isPlaying) return;
        StartCoroutine(FlashRoutine(onComplete));
    }

    IEnumerator FlashRoutine(System.Action onComplete)
    {
        isPlaying = true;

        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            SetAlpha(t / fadeInTime);
            yield return null;
        }

        SetAlpha(1f);

        yield return new WaitForSeconds(holdTime);

        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            SetAlpha(1f - t / fadeInTime);
            yield return null;
        }

        SetAlpha(0f);

        isPlaying = false;

        if (onComplete != null) onComplete();
    }

    void SetAlpha(float a)
    {
        Color c = panel.color;
        c.a = a;
        panel.color = c;
    }
}