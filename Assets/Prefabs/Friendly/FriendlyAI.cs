using System.Collections;
using System.Linq;
using UnityEngine;

public class FriendlyAI : MonoBehaviour
{
    public float Range = 0f;

    public float AttackDelay = 0.5f;

    private Transform target;
    private Attackable attackComponent;
    private EnemySpawner spawner;
    public int CurrentUpgrade;
    public bool canAttack = true;
    LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        attackComponent = GetComponent<Attackable>();        
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        var upgradeCheck = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        if (tag == "Archer" && upgradeCheck.ArcherUpgrade > 0)
        {
            CurrentUpgrade = upgradeCheck.ArcherUpgrade;
            var currentUpgrade = upgradeCheck.Archer[upgradeCheck.ArcherUpgrade];
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = currentUpgrade.sprite;
            GetComponent<Attackable>().Damage = currentUpgrade.HitPoint;
            GetComponent<DestroyableEntity>().HealthPoint = currentUpgrade.Health;
        }
        else if (tag == "Knight" && upgradeCheck.KnightUpgrade > 0)
        {
            CurrentUpgrade = upgradeCheck.KnightUpgrade;
            var currentUpgrade = upgradeCheck.Knight[upgradeCheck.KnightUpgrade];
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentUpgrade.sprite;
            GetComponent<Attackable>().Damage = currentUpgrade.HitPoint;
            GetComponent<DestroyableEntity>().HealthPoint = currentUpgrade.Health;
        }
        else if (tag == "Mage" && upgradeCheck.MageUpgrade > 0)
        {
            CurrentUpgrade = upgradeCheck.MageUpgrade;
            var currentUpgrade = upgradeCheck.Mage[upgradeCheck.MageUpgrade];
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentUpgrade.sprite;
            GetComponent<Attackable>().Damage = currentUpgrade.HitPoint;
            GetComponent<DestroyableEntity>().HealthPoint = currentUpgrade.Health;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canAttack && spawner.SpawnedEnemy.Count > 0)
        {
            if (target == null)
            {
                var collider = Physics2D.OverlapCircleAll(transform.position, Range, enemyMask);
                if (collider.Length != 0)
                    target = collider[0].transform;
            }
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

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        canAttack = true;
    }
}
