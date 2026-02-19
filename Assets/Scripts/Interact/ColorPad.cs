using System.Collections;
using UnityEngine;

public class ColorPad : MonoBehaviour
{
    public int colorIndex;
    public Room01Controller controller;

    SpriteRenderer sr;
    BoxCollider2D col;

    bool canPress = true;
    bool animating;
    Vector3 baseScale;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        baseScale = transform.localScale;
    }

    public void SetPad(bool enabled, Color color)
    {
        if (sr != null) sr.color = color;
        if (col != null) col.enabled = enabled;
        canPress = enabled;
        if (!enabled) StopAllCoroutines();
        transform.localScale = baseScale;
        animating = false;
    }

    public void Pulse()
    {
        if (animating) return;
        StartCoroutine(PulseRoutine());
    }

    IEnumerator PulseRoutine()
    {
        animating = true;
        transform.localScale = baseScale * 0.9f;
        yield return new WaitForSeconds(0.06f);
        transform.localScale = baseScale * 1.05f;
        yield return new WaitForSeconds(0.07f);
        transform.localScale = baseScale;
        animating = false;
    }

    public void Flash(Color c, float t)
    {
        StartCoroutine(FlashRoutine(c, t));
    }

    IEnumerator FlashRoutine(Color c, float t)
    {
        if (sr == null) yield break;
        var old = sr.color;
        sr.color = c;
        yield return new WaitForSeconds(t);
        sr.color = old;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canPress) return;
        if (controller == null) return;
        if (!other.CompareTag("Player")) return;

        Pulse();
        controller.Press(colorIndex, this);
    }
}
