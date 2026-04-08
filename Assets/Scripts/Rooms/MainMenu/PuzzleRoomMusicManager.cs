using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleRoomMusicManager : MonoBehaviour
{
    public static PuzzleRoomMusicManager I;

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
        bool isPuzzleRoom =
            sceneName == "S20_Room_01" ||
            sceneName == "S21_Room_02" ||
            sceneName == "S22_Room_03" ||
            sceneName == "S23_Room_04" ||
            sceneName == "S24_Room_05" ||
            sceneName == "S25_Room_06" ||
            sceneName == "S26_Room_07" ||
            sceneName == "S27_Room_08";

        if (isPuzzleRoom)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}