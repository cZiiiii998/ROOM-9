using UnityEngine;

public class LaserHit : MonoBehaviour
{
    public Room03Controller controller;

    void Awake()
    {
        if (controller == null) controller = FindObjectOfType<Room03Controller>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag("Player")) return;
        if (controller == null) controller = FindObjectOfType<Room03Controller>();
        if (controller == null) return;
        controller.Fail();
    }
}
