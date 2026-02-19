using System.Collections;
using UnityEngine;

public class Room02Controller : MonoBehaviour
{
    public Transform playerRoot;
    public Transform startPoint;
    public Transform endPoint;
    public float endRadius = 0.8f;
    public float failFreeze = 0.15f;

    Rigidbody2D playerRb;

    bool cleared;
    bool busy;

    void Start()
    {
        if (playerRoot != null) playerRb = playerRoot.GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        if (cleared) return;
        if (playerRoot == null || endPoint == null) return;

        var d = Vector2.Distance(playerRoot.position, endPoint.position);
        if (d <= endRadius) cleared = true;
    }

    public void StepOn(Room02Tile tile)
    {
        if (cleared) return;
        if (busy) return;
        if (tile == null) return;

        if (tile.isSafe) return;

        if (!tile.IsCollapsed())
        {
            tile.Collapse();
        }

        StartCoroutine(FailRoutine());
    }

    IEnumerator FailRoutine()
    {
        busy = true;
        yield return new WaitForSeconds(failFreeze);

        if (startPoint != null)
        {
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
                playerRb.position = startPoint.position;
            }
            else if (playerRoot != null)
            {
                playerRoot.position = startPoint.position;
            }
        }

        busy = false;
    }
}
