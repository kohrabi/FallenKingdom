using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UpgradeManager;

public class FriendlyButton : MonoBehaviour
{
    public GameObject currentFriendly;
    public bool Woods = true;
    public int Type = 0;
    UpgradeManager upgradeManager;
    CanvasGroup canvas;

    public void Start()
    {
        upgradeManager = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        canvas = GetComponent<CanvasGroup>();

        transform.GetChild(2).GetComponent<TMP_Text>().text = upgradeManager.GetPrice(Type).ToString();
        if (!upgradeManager.PriceCheck(Woods, Type))
        {
            canvas.alpha = 0.2f;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
            canvas.alpha = 1f;
        }
    }


    public void OnEnable()
    {
        if (GetComponent<Button>() == null || upgradeManager == null || canvas == null)
            return;
        if (!upgradeManager.PriceCheck(Woods, Type))
        {
            canvas.alpha = 0.2f;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
            canvas.alpha = 1f;
        }
    }

    public void OnClick()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        player.PlacePrefabTemp(currentFriendly);
        GameObject.Find("ShopInterface").SetActive(false);
    }
}
