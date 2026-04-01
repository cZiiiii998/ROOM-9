using UnityEngine;

public class ReturnSpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointId;

    public string SpawnPointId => spawnPointId;
}