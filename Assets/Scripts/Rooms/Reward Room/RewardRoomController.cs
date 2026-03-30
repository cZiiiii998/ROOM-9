using UnityEngine;

public class RewardRoomController : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform keySpawnPoint;

    void Start()
    {
        Debug.Log("RewardRoomController Start");
        Debug.Log("GameState exists: " + (GameState.I != null));

        if (GameState.I == null) return;

        Debug.Log("nextRewardKey: " + GameState.I.nextRewardKey);

        if (GameState.I.nextRewardKey == GameState.KeyType.None) return;
        if (GameState.I.HasKey(GameState.I.nextRewardKey)) return;
        if (keyPrefab == null || keySpawnPoint == null) return;

        GameObject keyObj = Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);

        RewardKeyPickup pickup = keyObj.GetComponent<RewardKeyPickup>();
        if (pickup != null)
        {
            pickup.keyType = GameState.I.nextRewardKey;
        }

        Debug.Log("Key spawned");
    }
}