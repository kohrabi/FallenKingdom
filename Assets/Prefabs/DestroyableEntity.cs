using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEntity : MonoBehaviour
{
    public float HealthPoint = 4f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HealthPoint <= 0)
        {
            // play dead animation and wait until it is finished then destroy it.
            // This needs to be placed elsewhere for dead animation
            Object.Destroy(gameObject);
        }
    }

    public void Hit(float damage)
    {
        HealthPoint -= damage;
        // play hit animation
    }
}
