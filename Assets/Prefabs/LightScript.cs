using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private float Scale = 1f;
    public float ScaleRange = 0.29f;
    public float LightChangeScale = 1f;

    float nextTimeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Scale = transform.localScale.x;
        nextTimeScale = Time.time + LightChangeScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nextTimeScale < Time.time)
        {
            float randomScale = Random.Range(Scale - ScaleRange, Scale + ScaleRange);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(randomScale, randomScale), LightChangeScale);
            nextTimeScale = Time.time + LightChangeScale;
        }
    }
}
