using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyButton : MonoBehaviour
{
    public GameObject currentFriendly;
    public void OnClick()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().PlacePrefabTemp(currentFriendly);
        GameObject.Find("ShopInterface").SetActive(false);
    }
}
