using UnityEngine;
using UnityEngine.SceneManagement;

public class CorridorMusicManager : MonoBehaviour
{
    public static CorridorMusicManager I;

    public AudioSource audioSource;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        UpdateMusicState(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        if (I == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusicState(scene.name);
    }

    void UpdateMusicState(string sceneName)
    {
        bool shouldPlay = sceneName == "MainMenu" || sceneName == "S10_Corridor";

        if (shouldPlay)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }
}