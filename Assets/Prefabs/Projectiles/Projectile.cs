using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Range;
    public float Damage;

    float _remainingRange;
    float speed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _remainingRange = Range;
        rb = GetComponent<Rigidbody2D>();
        speed = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        if (Range == 0)
            return;
        if (_remainingRange <= 0)
        {
            Object.Destroy(gameObject);
        }
        else
        {
            _remainingRange = Mathf.Max(0, _remainingRange - speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: attack only the enemy not tree(maybe)
        if (other.gameObject.tag == transform.parent.tag)
            return;
        DestroyableEntity health = other.gameObject.GetComponent<DestroyableEntity>();
        if (health != null)
            health.Hit(Damage);
        Destroy(gameObject);
    }
}
