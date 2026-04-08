using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorToScene : MonoBehaviour
{
    public string targetSceneName;
    public bool playerInside;
    public bool requireAllKeys = true;

    public AudioSource openSound;
    public float loadDelay = 0.4f;

    bool isLoading;

    void Update()
    {
        if (!playerInside) return;
        if (isLoading) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CanOpen())
            {
                isLoading = true;

                if (openSound != null)
                {
                    openSound.Play();
                }

                Invoke(nameof(LoadScene), loadDelay);
            }
            else
            {
                Debug.Log("You need all three keys.");
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

    bool CanOpen()
    {
        if (!requireAllKeys) return true;
        if (GameState.I == null) return false;

        return GameState.I.keyA && GameState.I.keyB && GameState.I.keyC;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}