using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SwordButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.Find("MainMenu").transform.GetChild(2).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arrow.SetActive(true);
    }
     
    public void OnPointerExit(PointerEventData eventData)
    {
        arrow.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
