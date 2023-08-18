using System.Collections;
using UnityEngine;

public class DestroyableEntity : MonoBehaviour
{
    public float HealthPoint = 4f;
    public float currentHP;
    public Animator animator;
    SoundManager soundManager;
    public void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        currentHP = HealthPoint;
        if (animator == null)
            animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnEnable()
    {
        currentHP = HealthPoint;
    }

    void OnDisable()
    {
        if (GameObject.FindWithTag("Player") == null)
            return;
        if (tag == "Props" || tag == "Wall")
        {
            if (gameObject.name.Contains("Tree"))
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().WoodsCount += 2;
                soundManager.audio.pitch = 1.3f;
                soundManager.PlayClip(Random.Range(2, 5));
                gameObject.GetComponent<PropRespawn>().Restart();
            }
            else if (tag == "Wall")
            {
                soundManager.audio.pitch = 1.3f;
                soundManager.PlayClip(Random.Range(2, 5));
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().RocksCount += 2;
                soundManager.audio.pitch = 1.3f;
                soundManager.PlayClip(1);
                gameObject.GetComponent<PropRespawn>().Restart();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHP <= 0)
        {
            if (animator == null)
                Destroy(gameObject);
            else
                StartCoroutine(Dead());
            // play dead animation and wait until it is finished then destroy it.
            // This needs to be placed elsewhere for dead animation
            //Object.Destroy(gameObject);
        }
    }

    public void ChangeHP(float hp)
    {
        HealthPoint = hp;
        currentHP = hp;
    }

    public IEnumerator Dead()
    {
        animator.SetTrigger("Dead");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            yield return null;
        if (tag == "Props")
            enabled = false;
        else
            Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        currentHP -= damage;
        animator.SetTrigger("Damaged");
        if (tag == "Player")
        {
           StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().Shake(0.2f, 0.1f));
        }
        // play hit animation
    }
}
