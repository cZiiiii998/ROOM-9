using UnityEngine;

public class ObstacleHitSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float cooldown = 0.2f;

    float lastPlayTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (Time.time - lastPlayTime < cooldown) return;

        lastPlayTime = Time.time;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastPlayTime < cooldown) return;

        lastPlayTime = Time.time;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}