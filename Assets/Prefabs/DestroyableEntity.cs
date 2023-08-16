using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEntity : MonoBehaviour
{
    public float HealthPoint = 4f;
    public float currentHP;
    public Animator animator;

    public void Start()
    {
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
        if (tag == "Props")
        {
            if (gameObject.name.Contains("Tree"))
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().WoodsCount += 3;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().RocksCount += 2;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHP <= 0)
        {
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
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<PropRespawn>().Restart();
        }
        else
            Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        currentHP -= damage;
        animator.SetTrigger("Damaged");
        // play hit animation
    }
}
