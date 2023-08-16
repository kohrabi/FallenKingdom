using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeScript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        animator.Play("Equip");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Move(bool move)
    {
        animator.SetBool("isMoving", move);
    }

    public void PutAway()
    {
        animator.SetTrigger("PutAway");
        StartCoroutine(CheckPutAway());
    }

    public IEnumerator CheckPutAway()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("PickaxePutAway"))
            yield return null;
        gameObject.SetActive(false);
    }
}
