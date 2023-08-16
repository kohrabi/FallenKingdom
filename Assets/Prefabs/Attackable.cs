using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public Transform spawnPosition;

    public float Range = 200f;

    public GameObject bulletPrefab;
    public float bulletSpeed = 4f;
    public float blockMovement = 0.5f;
    public float Damage = 1f;

    public void Start()
    {
        if (spawnPosition == null)
            spawnPosition = transform;
    }

    public float Attack(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotQua = Quaternion.Euler(0, 0, rotation); 
            GameObject entity = Instantiate(bulletPrefab, spawnPosition.position, rotQua);
            Destroy(entity, 3f);
            entity.transform.SetParent(transform);
            entity.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
            entity.GetComponent<Projectile>().Damage = Damage;
            entity.GetComponent<Projectile>().SetRange(Range);
            entity.GetComponent<Projectile>().SetSpeed((direction.normalized * bulletSpeed).magnitude);
        }
        return blockMovement;
    }
}
