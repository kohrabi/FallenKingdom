using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
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
        if (animator != null)
            animator.Play("TorchEquip");
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
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("TorchPutAway"))
            yield return null;
        gameObject.SetActive(false);
    }
}
