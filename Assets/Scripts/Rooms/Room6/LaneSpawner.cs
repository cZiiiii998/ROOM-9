using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform spawnPoint;
    public int direction = 1;
    public float despawnX = 12f;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2.5f;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    private float timer;
    private float nextSpawn;

    void Start()
    {
        SetNextSpawn();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawn)
        {
            SpawnObstacle();
            timer = 0f;
            SetNextSpawn();
        }
    }

    void SetNextSpawn()
    {
        nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnObstacle()
    {
        GameObject obj = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);

        MovingObstacle mo = obj.GetComponent<MovingObstacle>();
        mo.direction = direction;
        mo.speed = Random.Range(minSpeed, maxSpeed);
        mo.despawnX = despawnX;
    }
}