using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject arrow;
    public GameObject canvas;
    public Material InvertColor;
    // Start is called before the first frame update

    public void Start()
    {
        InvertColor.SetFloat("_Threshold", 0);
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
        transform.GetChild(0).gameObject.GetComponent<Animation>().enabled = true;
        canvas.SetActive(true);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        var fade = canvas.transform.GetChild(0).gameObject;
        var color = fade.GetComponent<Image>().color;
        fade.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 1, 2f));
        yield return new WaitForSeconds(1f);
        GameObject.Find("kohrabi").SetActive(false);
        GameObject.Find("MainMenu").SetActive(false);
        GameObject.Find("Title").SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
    }
}
