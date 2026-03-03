using UnityEngine;

public class PushPuzzleManager : MonoBehaviour
{
    public Rigidbody2D leftBox;
    public Rigidbody2D rightBox;

    public Transform leftBoxStart;
    public Transform rightBoxStart;

    public GoalZone leftGoal;
    public GoalZone rightGoal;

    public BoxCollider2D gateCollider;
    public SpriteRenderer gateRenderer;

    public Transform gateMask;
    public float sinkTime = 1.2f;

    public Collider2D leftResetCollider;
    public Collider2D rightResetCollider;

    public bool completed;
    public bool resetEnabled = true;

    private Vector3 maskStartPos;
    private Vector3 maskEndPos;

    void Start()
    {
        completed = false;
        resetEnabled = true;

        if (gateCollider != null) gateCollider.enabled = true;
        if (gateRenderer != null) gateRenderer.enabled = true;

        if (leftResetCollider != null) leftResetCollider.enabled = true;
        if (rightResetCollider != null) rightResetCollider.enabled = true;

        if (gateMask != null && gateRenderer != null)
        {
            maskStartPos = gateMask.position;

            float h = gateRenderer.bounds.size.y;
            maskEndPos = maskStartPos + Vector3.down * (h + 0.3f);

            gateMask.position = maskStartPos;
        }
    }

    void Update()
    {
        if (completed) return;
        if (leftGoal == null || rightGoal == null) return;

        if (leftGoal.satisfied && rightGoal.satisfied)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        completed = true;
        resetEnabled = false;

        if (leftResetCollider != null) leftResetCollider.enabled = false;
        if (rightResetCollider != null) rightResetCollider.enabled = false;

        if (gateCollider != null) gateCollider.enabled = false;

        if (gateMask != null)
            StartCoroutine(MoveMask(maskStartPos, maskEndPos, sinkTime));
    }

    public void ResetBoxes()
    {
        if (!resetEnabled) return;

        ResetOne(leftBox, leftBoxStart);
        ResetOne(rightBox, rightBoxStart);

        if (leftGoal != null) leftGoal.satisfied = false;
        if (rightGoal != null) rightGoal.satisfied = false;

        completed = false;

        if (gateCollider != null) gateCollider.enabled = true;
        if (gateRenderer != null) gateRenderer.enabled = true;

        if (gateMask != null)
            StartCoroutine(MoveMask(gateMask.position, maskStartPos, sinkTime));

        if (leftResetCollider != null) leftResetCollider.enabled = true;
        if (rightResetCollider != null) rightResetCollider.enabled = true;
    }

    void ResetOne(Rigidbody2D rb, Transform start)
    {
        if (rb == null || start == null) return;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = start.position;
        rb.rotation = start.eulerAngles.z;
    }

    System.Collections.IEnumerator MoveMask(Vector3 from, Vector3 to, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / time);
            gateMask.position = Vector3.Lerp(from, to, p);
            yield return null;
        }

        gateMask.position = to;
    }
}