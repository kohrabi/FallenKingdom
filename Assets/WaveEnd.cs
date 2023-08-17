using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveEnd : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<TMP_Text>().text = (GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>().CurrentWave - 1).ToString();
    }
}
