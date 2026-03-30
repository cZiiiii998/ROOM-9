using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardDoorToScene : MonoBehaviour
{
    public string targetSceneName = "S30_RewardRoom";
    public GameState.KeyType rewardKeyType = GameState.KeyType.A;
    public bool canUse = false;

    bool playerInside;

    void Update()
    {
        if (!canUse) return;

        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (GameState.I != null)
            {
                GameState.I.nextRewardKey = rewardKeyType;
                Debug.Log("Set reward key: " + rewardKeyType);
            }

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