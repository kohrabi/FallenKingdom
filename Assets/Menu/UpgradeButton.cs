using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Refresh();
    }

    public void OnEnable()
    {
        if (GetComponent<Button>() == null || upgrade == null || canvas == null)
            return;

        Refresh();
    }

    public void Refresh()
    {
        if (!upgrade.PriceUpgradeCheck(Woods, Upgrade) || outofUpgrade)
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
        upgrade.UpgradeFriendly(Upgrade);
        upgrade.Sold(Woods, Upgrade);
        if (upgrade.CheckLastUpgrade(Upgrade))
        {
            GetComponent<CanvasGroup>().alpha = 0.2f;
            GetComponent<Button>().interactable = false;
            outofUpgrade = true;
            return;
        }
        transform.GetChild(1).GetComponent<TMP_Text>().text = upgrade.GetUpgradePrice(Upgrade).ToString();
        GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().PlayClip(6);
        RefreshAll();
    }

    public void RefreshAll()
    {
        Transform shop = GameObject.Find("Canvas").transform.GetChild(2);
        foreach (Transform ui in shop)
        {
            if (ui.childCount == 0 || ui.GetChild(0).GetComponent<FriendlyButton>() == null)
                continue;
            var friendly = ui.GetChild(0).GetComponent<FriendlyButton>();
            var upgrade = ui.GetChild(1).GetComponent<UpgradeButton>();
            if (friendly != null )
                friendly.Refresh();
            if (upgrade != null )
                upgrade.Refresh();
        }
    }
}

