using UnityEngine;

public class ResetPad : MonoBehaviour
{
    public PushPuzzleManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (manager == null) return;
        if (!manager.resetEnabled) return;

        manager.ResetBoxes();
    }
}