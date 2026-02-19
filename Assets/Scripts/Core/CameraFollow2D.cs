using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth = 12f;

    void LateUpdate()
    {
        if (target == null) return;

        var desired = target.position + offset;
        desired.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desired, 1f - Mathf.Exp(-smooth * Time.deltaTime));
    }
}
