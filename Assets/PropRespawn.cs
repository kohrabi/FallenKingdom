using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRespawn : MonoBehaviour
{
    public int RespawnAfter = 1;
    public int Remaining = 1;

    public void Restart()
    {
        Remaining = RespawnAfter;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<DestroyableEntity>().enabled = false;
        GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>().propRespawns.Add(this);
    }

    public bool CheckRespawn()
    {
        Remaining--;
        if (Remaining <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<DestroyableEntity>().enabled = true;
            gameObject.GetComponent<DestroyableEntity>().currentHP = gameObject.GetComponent<DestroyableEntity>().HealthPoint;
            return true;
        }
        return false;
    }
}
