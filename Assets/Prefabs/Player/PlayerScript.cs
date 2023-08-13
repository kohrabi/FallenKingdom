using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MovementSpeed = 6f;

    private Rigidbody2D rb;
    public Animator animator;
    public Vector2 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        // Changing State Between moving and idle
        animator.SetBool("isMoving", moveDir != Vector2.zero);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * MovementSpeed, moveDir.y * MovementSpeed);
    }
}
