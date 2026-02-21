using UnityEngine;

public class FakeDoorInteractTeleport : MonoBehaviour
{
    public Transform startPoint;
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange;
    private Transform player;

    private void Update()
    {
        if (!playerInRange) return;
        if (Input.GetKeyDown(interactKey))
        {
            player.position = startPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        player = other.transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        player = null;
    }
}