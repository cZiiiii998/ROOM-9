using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMove = new Vector2(0f, 1f);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
        {
            movement.y = 0;
        }

        bool isMoving = movement != Vector2.zero;

        if (isMoving)
        {
            lastMove = movement.normalized;
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", lastMove.x);
        animator.SetFloat("moveY", lastMove.y);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}