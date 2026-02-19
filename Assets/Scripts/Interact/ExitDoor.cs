using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string targetSceneName = "S99_End";
    bool playerInside;

    void Update()
    {
        if (!playerInside) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        var s = GameState.I;
        if (s == null) return;

        if (s.HasAllKeys())
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInside = false;
    }
}
