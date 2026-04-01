using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSpawn : MonoBehaviour
{
    void Start()
    {
        MoveToReturnPointIfNeeded();
    }

    void MoveToReturnPointIfNeeded()
    {
        if (GameState.I == null) return;
        if (string.IsNullOrEmpty(GameState.I.returnSpawnPointId)) return;

        ReturnSpawnPoint[] points = FindObjectsOfType<ReturnSpawnPoint>();

        foreach (ReturnSpawnPoint point in points)
        {
            if (point.SpawnPointId == GameState.I.returnSpawnPointId)
            {
                transform.position = point.transform.position;
                break;
            }
        }
    }
}