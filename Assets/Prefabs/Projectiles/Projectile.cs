using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] // This is public for Attackable to change the projectile Range when it hit
    public float Range;
    [HideInInspector] // This is public for Attackable to change the projectile damage when it hit
    public float Damage;

    float _remainingRange;
    float speed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetRange(float range)
    {
        Range = range;
        _remainingRange = Range;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void FixedUpdate()
    {
        if (Range == 0)
            return;
        if (_remainingRange <= 0.01f)
        {
            Destroy(gameObject);
        }
        else
        {
            _remainingRange = _remainingRange - speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: attack only the enemy not tree(maybe)
        // this shit is stupid omgggg
        if (transform.parent == null)
            return;
        if (other.tag == "King" && transform.parent.tag != "Enemy")
            return;
        if (other.tag == "Wall" && transform.parent.tag != "Enemy")
            return;
        if (other.gameObject.tag == transform.parent.tag)
            return;
        if (other.gameObject.tag == "Props" && transform.parent.tag != "Player")
            return;
        if ((transform.parent.tag == "Archer" || transform.parent.tag == "Knight" || transform.parent.tag == "Mage") 
                && other.gameObject.tag == "Player")
            return;
        if ((transform.parent.tag == "Archer" || transform.parent.tag == "Knight" || transform.parent.tag == "Mage") &&
            (other.transform.tag == "Archer" || other.transform.tag == "Knight" || other.transform.tag == "Mage"))
            return;
        if (transform.parent.tag == "Player" && (other.transform.tag == "Archer" || other.transform.tag == "Knight" || other.transform.tag == "Mage"))
            return;
        DestroyableEntity health = other.gameObject.GetComponent<DestroyableEntity>();
        if (health != null)
            health.Hit(Damage);
        Destroy(gameObject);
    }
}
