using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneReset : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>().ChangeZone(Mathf.Max(transform.position.x, transform.position.y));
    }
}
