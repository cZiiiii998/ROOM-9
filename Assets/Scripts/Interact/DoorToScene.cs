using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToScene : MonoBehaviour
{
    public string targetSceneName = "S20_Room_01";
    public bool canUse = false;

    public bool useSavedReturnScene;
    public string returnSceneName;
    public string returnSpawnPointId;

    public AudioSource openSound;
    public float loadDelay = 0.3f;

    bool playerInside;
    bool isLoading;

    void Update()
    {
        if (!canUse) return;
        if (!playerInside) return;
        if (isLoading) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        isLoading = true;

        if (openSound != null)
        {
            openSound.Play();
        }

        Invoke(nameof(LoadDoorScene), loadDelay);
    }

    void LoadDoorScene()
    {
        if (GameState.I != null)
        {
            if (!useSavedReturnScene)
            {
                GameState.I.returnSceneName = returnSceneName;
                GameState.I.returnSpawnPointId = returnSpawnPointId;
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                if (!string.IsNullOrEmpty(GameState.I.returnSceneName))
                {
                    SceneManager.LoadScene(GameState.I.returnSceneName);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}