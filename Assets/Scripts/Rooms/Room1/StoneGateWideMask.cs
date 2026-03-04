using UnityEngine;

public class StoneGateWideMask : MonoBehaviour
{
    public SpriteRenderer[] walls;
    public Collider2D[] wallColliders;
    public Transform mask;
    public float sinkTime = 1.2f;
    public float extraSink = 0.3f;

    Vector3 maskStart;
    Vector3 maskEnd;
    bool opened;

    void Awake()
    {
        if (mask != null) maskStart = mask.position;

        float h = 1f;
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] == null) continue;
            h = walls[i].bounds.size.y;
            break;
        }

        maskEnd = maskStart + Vector3.down * (h + extraSink);

        CloseInstant();
    }

    public void Open()
    {
        if (opened) return;
        opened = true;

        if (wallColliders != null)
        {
            for (int i = 0; i < wallColliders.Length; i++)
                if (wallColliders[i] != null) wallColliders[i].enabled = false;
        }

        if (mask != null) StopAllCoroutines();
        if (mask != null) StartCoroutine(MoveMask(maskStart, maskEnd));
    }

    public void CloseInstant()
    {
        opened = false;

        if (walls != null)
        {
            for (int i = 0; i < walls.Length; i++)
                if (walls[i] != null) walls[i].enabled = true;
        }

        if (wallColliders != null)
        {
            for (int i = 0; i < wallColliders.Length; i++)
                if (wallColliders[i] != null) wallColliders[i].enabled = true;
        }

        if (mask != null) mask.position = maskStart;
    }

    System.Collections.IEnumerator MoveMask(Vector3 from, Vector3 to)
    {
        float t = 0f;

        while (t < sinkTime)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / sinkTime);
            mask.position = Vector3.Lerp(from, to, p);
            yield return null;
        }

        mask.position = to;

        if (walls != null)
        {
            for (int i = 0; i < walls.Length; i++)
                if (walls[i] != null) walls[i].enabled = false;
        }
    }
}