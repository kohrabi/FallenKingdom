using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public float CountDuration = 1f;
    TMP_Text text;
    float currentValue = 0;
    float targetValue = 0;
    Coroutine count;
    public bool woods = true;
    PlayerScript player;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentValue = float.Parse(text.text);
        targetValue = currentValue;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>(); 
    }

    private void FixedUpdate()
    {
        int value = woods ? player.WoodsCount : player.RocksCount;
        if (currentValue != value)
        {
            SetTarget(value);
        }
    }

    IEnumerator CountTo(float targetValue)
    {
        var rate = Mathf.Abs(targetValue - currentValue) / CountDuration;
        while (currentValue != targetValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.fixedDeltaTime);
            text.text = ((int)currentValue).ToString();
            yield return null;
        }    
    }

    public void AddValue(float value)
    {
        targetValue += value;
        if (count != null)
            StopCoroutine(count);
        count = StartCoroutine(CountTo(targetValue));
    }

    public void SetTarget(float target)
    {
        targetValue = target;
        if (count != null)
            StopCoroutine(count);
        count = StartCoroutine(CountTo(targetValue));
    }
}
