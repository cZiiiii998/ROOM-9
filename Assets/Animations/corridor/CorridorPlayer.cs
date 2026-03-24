using UnityEngine;

public class CorridorPlayer : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private float facing = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0f)
        {
            facing = 1f;
        }
        else if (moveInput < 0f)
        {
            facing = -1f;
        }

        animator.SetBool("isMoving", moveInput != 0f);
        animator.SetFloat("direction", facing);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}