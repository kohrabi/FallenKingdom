using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Walls : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int facing = 0;

    WallManager wallManager;

    // Start is called before the first frame update
    void Start()
    {
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        GetComponent<DestroyableEntity>().HealthPoint = wallManager.WallFaces[wallManager.CurrentUpgrade].Health;
    }

    public void ChangeFace()
    {
        facing = CircleBack(facing + 1);
        spriteRenderer.sprite = wallManager.WallFaces[wallManager.CurrentUpgrade].faces[facing];
        if (facing == 2)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    public void ChangeUpgrade(int upgrade)
    {
        spriteRenderer.sprite = wallManager.WallFaces[upgrade].faces[facing];
        if (facing == 2)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        GetComponent<DestroyableEntity>().HealthPoint = wallManager.WallFaces[upgrade].Health;
    }


    private int CircleBack(int i)
    {
        if (i >= 4)
            return 0;
        return i;
    }
}
