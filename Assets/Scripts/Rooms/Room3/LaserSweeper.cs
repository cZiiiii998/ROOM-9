using UnityEngine;

public class LaserSweeper : MonoBehaviour
{
    public Vector3 localA;
    public Vector3 localB;
    public float period = 1.4f;
    public float phase;

    void Update()
    {
        float t = (Time.time + phase) / period;
        float u = Mathf.PingPong(t, 1f);
        transform.localPosition = Vector3.Lerp(localA, localB, u);
    }
}
