using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Material InvertColor;
    public float DayTime = 120f;
    private EnemySpawner spawner;
    public float DayNightChangeSpeed = 0.04f;
    public float InvertColorThreshold = 0.7f;

    public float dayTime = 0;

    GameObject cam;
    GameObject lightCam;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        InvertColor.SetFloat("_Threshold", InvertColorThreshold);
        dayTime = Time.time + DayTime;
        cam = GameObject.FindWithTag("MainCamera");
        lightCam = cam.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float threshold = InvertColor.GetFloat("_Threshold");
        
        if (spawner.isDay)
        {
            // switch to day time
            InvertColor.SetFloat("_Threshold", Mathf.Lerp(threshold, InvertColorThreshold, DayNightChangeSpeed));
            if (threshold >= InvertColorThreshold - 0.04f)
            {
                dayTime = Time.time + dayTime;
                spawner.isDay = false;
                cam.GetComponent<LightingCamera>().enabled = false;
                lightCam.SetActive(false);
            }
        }
        // if out of daytime && is not night time
        else if (dayTime <= Time.time && !spawner.isNight)
        {
            // switch to night time
            InvertColor.SetFloat("_Threshold", Mathf.Lerp(threshold, 0, DayNightChangeSpeed));
            if (threshold <= DayNightChangeSpeed + DayNightChangeSpeed / 10f)
            {
                spawner.SwitchNightTime();
                cam.GetComponent<LightingCamera>().enabled = true;
                lightCam.SetActive(true);
            }
        }
    }
}
