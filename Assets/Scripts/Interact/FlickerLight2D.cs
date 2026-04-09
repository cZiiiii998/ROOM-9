using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight2D : MonoBehaviour
{
    public Light2D targetLight;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1f;
    public float intervalMin = 0.06f;
    public float intervalMax = 0.14f;

    float timer;
    float nextInterval;

    void Start()
    {
        nextInterval = Random.Range(intervalMin, intervalMax);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextInterval)
        {
            targetLight.intensity = Random.Range(minIntensity, maxIntensity);
            timer = 0f;
            nextInterval = Random.Range(intervalMin, intervalMax);
        }
    }
}