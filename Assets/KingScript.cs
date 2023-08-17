using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScrip : MonoBehaviour
{
    public GameObject gameOverUI;

    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void OnDestroy()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
}
