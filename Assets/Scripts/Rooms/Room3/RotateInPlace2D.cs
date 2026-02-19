using UnityEngine;

public class RotateInPlace2D : MonoBehaviour
{
    public float speed = 180f;

    Vector3 centerPos;

    void Start()
    {
        centerPos = transform.position;
    }

    void Update()
    {
        transform.RotateAround(centerPos, Vector3.forward, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z);
    }
}
