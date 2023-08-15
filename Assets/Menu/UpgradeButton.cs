using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public int Upgrade = 0;
    public void OnClick()
    {
        GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().UpgradeFriendly(Upgrade);
    }
}
