using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour
{
    private bool playerInside;

    void Update()
    {
        if (!playerInside) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (GameState.I == null) return;
        if (string.IsNullOrEmpty(GameState.I.returnSceneName)) return;

        SceneManager.LoadScene(GameState.I.returnSceneName);
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