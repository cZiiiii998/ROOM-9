using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStairsChoice : MonoBehaviour
{
    public enum ChoiceType
    {
        Up,
        Down
    }

    [Header("Choice")]
    [SerializeField] private ChoiceType choiceType;

    [Header("UI References")]
    [SerializeField] private GameObject endingPanelObject;
    [SerializeField] private Image endingPanelImage;
    [SerializeField] private TextMeshProUGUI endingText;
    [SerializeField] private TextMeshProUGUI continueText;

    [Header("Scene Names")]
    [SerializeField] private string restartSceneName;
    [SerializeField] private string mainMenuSceneName;

    [Header("Ending Text")]
    [TextArea(6, 20)]
    [SerializeField] private string upEndingText;

    [TextArea(6, 20)]
    [SerializeField] private string downEndingText;

    [TextArea(2, 5)]
    [SerializeField] private string continuePrompt = "Press any key to continue";

    [Header("Timing")]
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float typewriterSpeed = 0.03f;
    [SerializeField] private float continuePromptDelay = 0.3f;
    [SerializeField] private float maxPanelAlpha = 0.9f;

    private bool playerInside;
    private bool triggered;

    void Start()
    {
        SetupInitialUIState();
    }

    void Update()
    {
        if (triggered) return;
        if (!playerInside) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        triggered = true;
        StartCoroutine(PlayEndingSequence());
    }

    void SetupInitialUIState()
    {
        if (endingPanelObject != null)
        {
            endingPanelObject.SetActive(true);
        }

        if (endingPanelImage != null)
        {
            Color c = endingPanelImage.color;
            c.a = 0f;
            endingPanelImage.color = c;
        }

        if (endingText != null)
        {
            endingText.text = "";
            Color c = endingText.color;
            c.a = 1f;
            endingText.color = c;
        }

        if (continueText != null)
        {
            continueText.text = "";
            Color c = continueText.color;
            c.a = 0f;
            continueText.color = c;
        }
    }

    IEnumerator PlayEndingSequence()
    {
        Time.timeScale = 0f;

        yield return StartCoroutine(FadeInPanel());

        string targetText = choiceType == ChoiceType.Up ? upEndingText : downEndingText;

        if (endingText != null)
        {
            yield return StartCoroutine(TypeText(endingText, targetText));
        }

        yield return new WaitForSecondsRealtime(continuePromptDelay);

        if (continueText != null)
        {
            continueText.text = continuePrompt;
            yield return StartCoroutine(FadeInText(continueText, 0.4f));
        }

        yield return new WaitUntil(() => Input.anyKeyDown);

        Time.timeScale = 1f;

        if (choiceType == ChoiceType.Up)
        {
            if (!string.IsNullOrEmpty(mainMenuSceneName))
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
            else
            {
                Debug.Log("Main menu scene name is empty.");
            }
        }
        else
        {
            if (GameState.I != null)
            {
                GameState.I.ResetRun();
            }

            if (!string.IsNullOrEmpty(restartSceneName))
            {
                SceneManager.LoadScene(restartSceneName);
            }
            else
            {
                Debug.Log("Restart scene name is empty.");
            }
        }
    }

    IEnumerator FadeInPanel()
    {
        if (endingPanelImage == null) yield break;

        float time = 0f;
        Color startColor = endingPanelImage.color;
        Color endColor = endingPanelImage.color;
        startColor.a = 0f;
        endColor.a = maxPanelAlpha;

        endingPanelImage.color = startColor;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / fadeDuration);
            endingPanelImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        endingPanelImage.color = endColor;
    }

    IEnumerator TypeText(TextMeshProUGUI textComponent, string fullText)
    {
        textComponent.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSecondsRealtime(typewriterSpeed);
        }
    }

    IEnumerator FadeInText(TextMeshProUGUI textComponent, float duration)
    {
        float time = 0f;
        Color color = textComponent.color;
        color.a = 0f;
        textComponent.color = color;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / duration);
            color.a = Mathf.Lerp(0f, 1f, t);
            textComponent.color = color;
            yield return null;
        }

        color.a = 1f;
        textComponent.color = color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}