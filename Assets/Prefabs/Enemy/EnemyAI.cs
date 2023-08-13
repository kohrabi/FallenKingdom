using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float MovementSpeed = 6f;
    public Transform target;
    public float AttackRange = 5f;
    public float AttackDelay = 3f;

    private Rigidbody2D rb;
    private Attackable attackComponent;

    bool canAttack = true;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackComponent = GetComponent<Attackable>();
    }

    // attack, if player in range of (light/king) than attack king
    // else attack player

    // Update is called once per fram
    void FixedUpdate()
    {
        if (target == null)
            return;
        Vector2 direction = target.position - transform.position;
        if (canMove)
            rb.velocity = direction.normalized * MovementSpeed;
        else
            rb.velocity = Vector2.zero;
        if (direction.magnitude <= AttackRange && canAttack)
        {
            float moveBlock = attackComponent.Attack(direction);
            canAttack = false;
            canMove = false;
            StartCoroutine(DelayAttack());
            StartCoroutine(BlockMovement(moveBlock));
        }
    }

    private IEnumerator BlockMovement(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        canAttack = true;
    }
}
