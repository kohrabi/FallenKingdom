using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UpgradeManager;

public class UpgradeButton : MonoBehaviour
{
    UpgradeManager upgrade;
    public int Upgrade = 0;
    public bool Woods = true;
    CanvasGroup canvas;
    bool outofUpgrade = false;

    public void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        upgrade = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        transform.GetChild(1).GetComponent<TMP_Text>().text = upgrade.GetUpgradePrice(Upgrade).ToString();
        if (!upgrade.PriceCheck(Woods, Upgrade) || outofUpgrade)
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
        if (GetComponent<Button>() == null || upgrade == null || canvas == null)
            return;

        if (!upgrade.PriceCheck(Woods, Upgrade) || outofUpgrade)
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
        upgrade.Sold(Woods, Upgrade);
        upgrade.UpgradeFriendly(Upgrade);
        if (upgrade.CheckLastUpgrade(Upgrade))
        {
            GetComponent<CanvasGroup>().alpha = 0.2f;
            GetComponent<Button>().interactable = false;
            outofUpgrade = true;
            return;
        }
        transform.GetChild(1).GetComponent<TMP_Text>().text = upgrade.GetUpgradePrice(Upgrade).ToString();
    }
}
