using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float Maximum;
    public float Current;

    TimeManager timeManager;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        timeManager = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();
        Maximum = timeManager.DayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.IsDayTime)
        {
            Current = Time.time;
            Maximum = timeManager.dayTime;
            GetCurrentFill();
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = Current / Maximum;
        image.fillAmount = fillAmount;
    }
}
