using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float MovementSpeed = 6f;
    public Transform originTarget;
    public Transform target;
    public float AttackRange = 5f;
    public float AttackDelay = 3f;

    private Rigidbody2D rb;
    private Attackable attackComponent;

    EnemySpawner zone;

    Transform player;
    bool canAttack = true;
    bool canMove = true;
    bool changeTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originTarget = GameObject.FindWithTag("King").transform;
        attackComponent = GetComponent<Attackable>();
        zone = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        var playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // attack, if player in range of (light/king) than attack king
    // else attack player

    // Update is called once per fram
    void FixedUpdate()
    {
        if (target == null || (target.position - transform.position).magnitude > 7f || changeTarget)
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 8f);
            if (collider.Length != 0)
            {
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.tag != "Props" && collider[i].gameObject.tag != "Enemy")
                    {
                        target = collider[i].transform;
                        break;
                    }
                }
                if (target == null && collider[0].gameObject.tag != "Props" && collider[0].gameObject.tag != "Enemy")
                    target = collider[0].transform;
                StartCoroutine(ChangeTarget(3f));
            }
        }
        if (target == null)
            target = originTarget;
        if (player != null && player.position.magnitude > zone.PlayableZone + 10f)
            target = player;
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

    private IEnumerator ChangeTarget(float time)
    {
        yield return new WaitForSeconds(time);
        changeTarget = true;
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
