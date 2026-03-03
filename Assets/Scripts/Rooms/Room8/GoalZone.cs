using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public Rigidbody2D targetBox;
    public bool satisfied;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody == targetBox)
            satisfied = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody == targetBox)
            satisfied = false;
    }
}