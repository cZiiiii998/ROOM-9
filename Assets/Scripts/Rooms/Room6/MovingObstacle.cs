using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 3f;
    public int direction = 1;
    public float despawnX = 12f;

    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void Update()
    {
        Vector3 p = transform.position;
        p.x += direction * speed * Time.deltaTime;
        p.y = fixedY;
        transform.position = p;

        if (direction == 1 && transform.position.x > despawnX)
            Destroy(gameObject);

        if (direction == -1 && transform.position.x < -despawnX)
            Destroy(gameObject);
    }
}