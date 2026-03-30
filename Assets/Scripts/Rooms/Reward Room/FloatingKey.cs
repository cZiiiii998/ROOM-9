using UnityEngine;

public class FloatingKey : MonoBehaviour
{
    public float floatAmplitude = 0.1f;
    public float floatSpeed = 2f;

    public bool enableRotation = true;
    public float rotationSpeed = 30f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);

        if (enableRotation)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}