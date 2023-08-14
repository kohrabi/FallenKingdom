using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAI : MonoBehaviour
{
    public float Range = 0f;

    public float AttackDelay = 0.5f;

    private Transform target;
    private Attackable attackComponent;
    private EnemySpawner spawner;

    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        attackComponent = GetComponent<Attackable>();        
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canAttack && spawner.SpawnedEnemy.Count > 0)
        {
            if (target == null)
                target = GetClosestEnemy();
            if (target != null)
            {
                Vector2 direction = target.position - transform.position;
                if ((Range == 0) || (direction.magnitude <= Range))
                {
                    attackComponent.Attack(target.position - transform.position);
                    canAttack = false;
                    StartCoroutine(DelayAttack());
                }
            }
        }
    }

    private Transform GetClosestEnemy()
    {
        Transform closest = null;
        float distance = float.MaxValue;
        foreach (GameObject enemy in spawner.SpawnedEnemy)
        {
            float currentDis = (transform.position - enemy.transform.position).magnitude;
            if (distance > currentDis)
            {
                distance = currentDis;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        canAttack = true;
    }
}
