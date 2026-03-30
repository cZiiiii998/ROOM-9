using UnityEngine;

public class RewardKeyPickup : MonoBehaviour
{
    public GameState.KeyType keyType;
    bool playerInside;

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameState.I != null)
            {
                GameState.I.GiveKey(keyType);
                GameState.I.nextRewardKey = GameState.KeyType.None;
            }

            Destroy(gameObject);
        }
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