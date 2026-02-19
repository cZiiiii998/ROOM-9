using System.Collections;
using UnityEngine;

public class Room03Controller : MonoBehaviour
{
    public Transform playerRoot;
    public Transform startPoint;
    public float failFreeze = 0.12f;

    Rigidbody2D rb;
    bool busy;

    void Start()
    {
        if (playerRoot != null) rb = playerRoot.GetComponentInChildren<Rigidbody2D>();
    }

    public void Fail()
    {
        if (busy) return;
        StartCoroutine(FailRoutine());
    }

    IEnumerator FailRoutine()
    {
        busy = true;
        yield return new WaitForSeconds(failFreeze);

        if (startPoint != null)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.position = startPoint.position;
            }
            else if (playerRoot != null)
            {
                playerRoot.position = startPoint.position;
            }
        }

        busy = false;
    }
}
