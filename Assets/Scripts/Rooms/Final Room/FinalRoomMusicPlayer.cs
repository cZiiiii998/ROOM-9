using UnityEngine;

public class FinalRoomMusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}