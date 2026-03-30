using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    public string noteText;

    public SpriteRenderer worldNoteSpriteRenderer;
    public Sprite worldNoteSprite;

    public GameObject notePanel;
    public TextMeshProUGUI noteContentText;
    public Image notePanelImage;
    public Sprite openNoteSprite;

    private bool playerInRange;
    private bool isReading;

    void Start()
    {
        if (worldNoteSpriteRenderer != null && worldNoteSprite != null)
        {
            worldNoteSpriteRenderer.sprite = worldNoteSprite;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isReading)
            {
                OpenNote();
            }
            else
            {
                CloseNote();
            }
        }
    }

    void OpenNote()
    {
        isReading = true;
        notePanel.SetActive(true);

        if (noteContentText != null)
        {
            noteContentText.text = noteText;
        }

        if (notePanelImage != null && openNoteSprite != null)
        {
            notePanelImage.sprite = openNoteSprite;
            notePanelImage.SetNativeSize();
        }

        if (worldNoteSpriteRenderer != null)
        {
            worldNoteSpriteRenderer.enabled = false;
        }

        Time.timeScale = 0f;
    }

    void CloseNote()
    {
        isReading = false;
        notePanel.SetActive(false);

        if (worldNoteSpriteRenderer != null)
        {
            worldNoteSpriteRenderer.enabled = true;
        }

        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}