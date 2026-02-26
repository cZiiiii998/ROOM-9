using UnityEngine;

public class ResetOnHit : MonoBehaviour
{
    public Transform startPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            transform.position = startPoint.position;
        }
    }
}