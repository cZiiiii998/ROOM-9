using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class IntroLine
{
    public string content;
    public float fadeIn;
    public float hold;
    public float fadeOut;
}

public class IntroController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image blackPanel;
    public RectTransform flashGlow;
    public Image flashGlowImage;
    public GameObject player;
    public IntroLine[] lines;

    public float glowStartScale = 0.2f;
    public float glowEndScale = 8f;
    public float glowDuration = 1.5f;
    public float glowPeakAlpha = 1f;

    int currentLine = 0;
    float timer = 0f;

    enum State
    {
        FadeInText,
        HoldText,
        FadeOutText,
        GlowExpand,
        Finish
    }

    State state;

    void Start()
    {
        player.SetActive(false);

        blackPanel.gameObject.SetActive(true);
        flashGlow.gameObject.SetActive(true);

        Color bc = blackPanel.color;
        bc.a = 1f;
        blackPanel.color = bc;

        Color gc = flashGlowImage.color;
        gc.a = 0f;
        flashGlowImage.color = gc;

        flashGlow.localScale = Vector3.one * glowStartScale;

        if (lines.Length > 0)
        {
            text.text = lines[0].content;
        }
        else
        {
            text.text = "";
        }

        SetTextAlpha(0f);

        state = State.FadeInText;
        timer = 0f;
    }

    void Update()
    {
        if (lines.Length == 0) return;

        timer += Time.deltaTime;

        if (state == State.FadeInText)
        {
            float t = timer / lines[currentLine].fadeIn;
            SetTextAlpha(Mathf.Clamp01(t));

            if (timer >= lines[currentLine].fadeIn)
            {
                timer = 0f;
                state = State.HoldText;
                SetTextAlpha(1f);
            }
        }
        else if (state == State.HoldText)
        {
            if (timer >= lines[currentLine].hold)
            {
                timer = 0f;
                state = State.FadeOutText;
            }
        }
        else if (state == State.FadeOutText)
        {
            float t = timer / lines[currentLine].fadeOut;
            SetTextAlpha(1f - Mathf.Clamp01(t));

            if (timer >= lines[currentLine].fadeOut)
            {
                timer = 0f;
                currentLine++;

                if (currentLine < lines.Length)
                {
                    text.text = lines[currentLine].content;
                    state = State.FadeInText;
                }
                else
                {
                    text.text = "";
                    state = State.GlowExpand;
                }
            }
        }
        else if (state == State.GlowExpand)
        {
            float t = timer / glowDuration;

            flashGlow.localScale = Vector3.Lerp(
                Vector3.one * glowStartScale,
                Vector3.one * glowEndScale,
                Mathf.Clamp01(t)
            );

            Color c = flashGlowImage.color;

            if (t < 0.15f)
            {
                c.a = Mathf.Lerp(0f, glowPeakAlpha, t / 0.15f);
            }
            else
            {
                c.a = Mathf.Lerp(glowPeakAlpha, 0f, (t - 0.15f) / 0.85f);
            }

            flashGlowImage.color = c;

            if (timer >= glowDuration)
            {
                timer = 0f;
                state = State.Finish;
            }
        }
        else if (state == State.Finish)
        {
            player.SetActive(true);
            blackPanel.gameObject.SetActive(false);
            flashGlow.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void SetTextAlpha(float a)
    {
        Color c = text.color;
        c.a = a;
        text.color = c;
    }
}