using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorToScene : MonoBehaviour
{
    public string targetSceneName;
    public bool playerInside;
    public bool requireAllKeys = true;

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CanOpen())
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.Log("You need all three keys.");
            }
        }
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