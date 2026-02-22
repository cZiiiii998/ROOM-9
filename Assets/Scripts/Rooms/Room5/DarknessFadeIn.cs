using UnityEngine;

public class DarknessFadeIn : MonoBehaviour
{
    public float targetAlpha = 0.9f;
    public float fadeDuration = 1.5f;

    private SpriteRenderer sr;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, targetAlpha, timer / fadeDuration);
            sr.color = new Color(0.15f, 0.15f, 0.15f, alpha);
        }
    }
}