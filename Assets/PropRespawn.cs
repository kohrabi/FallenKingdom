using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRespawn : MonoBehaviour
{
    public const int RespawnAfter = 1;
    public int Remaining = 1;

    public void Restart()
    {
        Remaining = RespawnAfter;
        GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>().propRespawns.Add(this);
    }

    public bool CheckRespawn()
    {
        Remaining--;
        if (Remaining <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<DestroyableEntity>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            return true;
        }
        return false;
    }
}
