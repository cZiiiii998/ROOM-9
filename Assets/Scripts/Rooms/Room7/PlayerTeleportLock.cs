using UnityEngine;

public class PlayerTeleportLock : MonoBehaviour
{
    public float cooldown = 0.25f;
    private float until;

    public bool CanTeleport()
    {
        return Time.time >= until;
    }

    public void Lock()
    {
        until = Time.time + cooldown;
    }
}