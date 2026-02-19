using UnityEngine;

public class Room02Tile : MonoBehaviour
{
    public bool isSafe;
    public Room02Controller controller;

    public Sprite normalSprite;
    public Sprite pitSprite;

    SpriteRenderer sr;
    Collider2D col;

    bool collapsed;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        if (controller == null) controller = FindObjectOfType<Room02Controller>();
        ApplyNormal();
    }

    public bool IsCollapsed()
    {
        return collapsed;
    }

    public void Collapse()
    {
        collapsed = true;

        if (sr != null)
        {
            if (pitSprite != null) sr.sprite = pitSprite;
            sr.color = Color.white;
            sr.enabled = true;
        }

        if (col != null)
        {
            col.enabled = true;
            col.isTrigger = true;
        }
    }

    void ApplyNormal()
    {
        collapsed = false;

        if (sr != null)
        {
            if (normalSprite != null) sr.sprite = normalSprite;
            sr.color = Color.white;
            sr.enabled = true;
        }

        if (col != null)
        {
            col.enabled = true;
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag("Player")) return;

        if (controller == null) controller = FindObjectOfType<Room02Controller>();
        if (controller == null) return;

        controller.StepOn(this);
    }
}
