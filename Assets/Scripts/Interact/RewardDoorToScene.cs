using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardDoorToScene : MonoBehaviour
{
    public string targetSceneName = "S30_RewardRoom";
    public GameState.KeyType rewardKeyType = GameState.KeyType.A;
    public bool canUse = false;

    public AudioSource openSound;
    public float loadDelay = 0.35f;

    bool playerInside;
    bool isLoading;

    void Update()
    {
        if (!canUse) return;
        if (!playerInside) return;
        if (isLoading) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isLoading = true;

            if (openSound != null)
            {
                openSound.Play();
            }

            if (GameState.I != null)
            {
                GameState.I.nextRewardKey = rewardKeyType;
                Debug.Log("Set reward key: " + rewardKeyType);
            }

            Invoke(nameof(LoadScene), loadDelay);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
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