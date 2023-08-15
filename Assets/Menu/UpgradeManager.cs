using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Serializable]
    public class Upgrade
    {
        public Sprite sprite;
        public float Health;
        public float HitPoint;
    }
    public Upgrade[] Archer;
    public Upgrade[] Knight;
    public Upgrade[] Mage;

    public int ArcherUpgrade = 0;
    public int KnightUpgrade = 0;
    public int MageUpgrade = 0;
    public bool UpgradeWallInWallManager = false;
    WallManager wallManager;

    public void Start()
    {
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();    
    }

    public void UpgradeFriendly(int up)
    {
       if (up == 0)
            UpgradeArcher();
       if (up == 1)
            UpgradeKnight();
       if (up == 2)
            UpgradeMage();
        if (up == 3)
            UpgradeWall();
    }

    public void UpgradeArcher()
    {
        ArcherUpgrade++;
        foreach (GameObject archer in GameObject.FindGameObjectsWithTag("Archer"))
        {
            archer.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Archer[ArcherUpgrade].sprite;
            archer.GetComponent<DestroyableEntity>().HealthPoint = Archer[ArcherUpgrade].Health;
            archer.GetComponent<Attackable>().Damage = Archer[ArcherUpgrade].HitPoint;
        }
    }

    public void UpgradeKnight()
    {
        KnightUpgrade++;
        foreach (GameObject knight in GameObject.FindGameObjectsWithTag("Knight"))
        {
            knight.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Knight[KnightUpgrade].sprite;
            knight.GetComponent<DestroyableEntity>().HealthPoint = Knight[KnightUpgrade].Health;
            knight.GetComponent<Attackable>().Damage = Knight[KnightUpgrade].HitPoint;
            // Add chance to block attack
        }
    }

    public void UpgradeMage()
    {
        MageUpgrade++;
        foreach (GameObject mage in GameObject.FindGameObjectsWithTag("Mage"))
        {
            mage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Mage[MageUpgrade].sprite;
            mage.GetComponent<DestroyableEntity>().HealthPoint = Mage[MageUpgrade].Health;
            mage.GetComponent<Attackable>().Damage = Mage[MageUpgrade].HitPoint;
            // add ability to heal more health
        }
    }

    public void UpgradeWall()
    {
        wallManager.CurrentUpgrade++;
        int currentUpgrade = wallManager.CurrentUpgrade;
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            wall.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wallManager.WallFaces[currentUpgrade].faces[wall.GetComponent<Walls>().facing];
            wall.GetComponent<DestroyableEntity>().HealthPoint = wallManager.WallFaces[currentUpgrade].Health;
        }
    }
}
