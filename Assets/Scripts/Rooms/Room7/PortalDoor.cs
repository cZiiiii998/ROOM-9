using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    public string doorId;
    public string targetDoorId;
    public Transform exitPoint;
    public KeyCode interactKey = KeyCode.E;

    private bool inRange;
    private Transform player;
    private PlayerTeleportLock teleportLock;
    private PortalNetwork network;

    void Awake()
    {
        network = FindFirstObjectByType<PortalNetwork>();
    }

    void Update()
    {
        if (!inRange) return;
        if (player == null) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (teleportLock != null && !teleportLock.CanTeleport()) return;

            Transform targetExit = network != null ? network.GetExitPoint(targetDoorId) : null;
            if (targetExit == null) return;

            if (teleportLock != null) teleportLock.Lock();
            player.position = targetExit.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = true;
        player = other.transform;
        teleportLock = other.GetComponent<PlayerTeleportLock>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = false;
        player = null;
        teleportLock = null;
    }
}