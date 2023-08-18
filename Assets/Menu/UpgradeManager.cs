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
        public int Price;
    }
    public Upgrade[] Archer;
    public Upgrade[] Knight;
    public Upgrade[] Mage;
    public Upgrade[] Torch;

    public int ArcherUpgrade = 0;
    public int KnightUpgrade = 0;
    public int MageUpgrade = 0;
    public bool UpgradeWallInWallManager = false;
    WallManager wallManager;
    PlayerScript player;

    public void Start()
    {
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    public void Sold(bool Woods, int up)
    {
        if (Woods)
        {
            if (up == 0)
                player.WoodsCount -= Archer[ArcherUpgrade].Price;
            if (up == 3)
                player.WoodsCount -= wallManager.WallFaces[wallManager.CurrentUpgrade].Price;
            if (up == 4)
                player.WoodsCount -= Torch[0].Price;
        }
        else
        {
            if (up == 1)
                player.RocksCount -= Knight[KnightUpgrade].Price;
            if (up == 2)
                player.RocksCount -= Mage[MageUpgrade].Price;
        }
    }

    public void GetPrice(string tag)
    {
        if (tag == "Archer")
            player.WoodsCount += Archer[ArcherUpgrade].Price;
        if (tag == "Knight")
            player.RocksCount += Knight[KnightUpgrade].Price;
        if (tag == "Mage")
            player.RocksCount += Mage[MageUpgrade].Price;
        if (tag == "Torch")
            player.WoodsCount += Torch[0].Price;
        if (tag == "Wall")
            player.WoodsCount += wallManager.WallFaces[wallManager.CurrentUpgrade].Price;

    }

    public bool Sold(string tag)
    {
        if (!PriceCheck(tag))
            return false;
        if (tag == "Archer")
            Sold(true, 0);
        if (tag == "Knight")
            Sold(false, 1);
        if (tag == "Mage")
            Sold(false, 2);
        if (tag == "Wall")
            Sold(true, 3);
        if (tag == "Torch")
            Sold(true, 4);
        return true;
    }

    public bool PriceCheck(string tag)
    {
        if (tag == "Archer")
            return PriceCheck(true, 0);
        if (tag == "Knight")
            return PriceCheck(false, 1);
        if (tag == "Mage")
            return PriceCheck(false, 2);
        if (tag == "Wall")
            return PriceCheck(true, 3);
        if (tag == "Torch")
            return PriceCheck(true, 4);
        return false;
    }

    public bool PriceCheck(bool Woods, int up)
    {
        if (Woods)
        {
            int money = player.WoodsCount;
            if (up == 0)
                return money >= Archer[ArcherUpgrade].Price;
            if (up == 3)
                return money >= wallManager.WallFaces[wallManager.CurrentUpgrade].Price;
            if (up == 4)
                return money >= Torch[0].Price;
        }
        else
        {
            int money = player.RocksCount;
            if (up == 1)
                return money >= Knight[KnightUpgrade].Price;
            if (up == 2)
                return money >= Mage[MageUpgrade].Price;
        }
        return false;
    }

    public bool PriceUpgradeCheck(bool Woods, int up)
    {
        if (Woods)
        {
            int money = player.WoodsCount;
            if (up == 0)
                return money >= Archer[ArcherUpgrade + 1].Price;
            if (up == 3)
                return money >= wallManager.WallFaces[wallManager.CurrentUpgrade + 1].Price;
        }
        else
        {
            int money = player.RocksCount;
            if (up == 1)
                return money >= Knight[KnightUpgrade + 1].Price;
            if (up == 2)
                return money >= Mage[MageUpgrade + 1].Price;
        }
        return false;
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

    public int GetPrice(int up)
    {
        if (up == 0)
            return Archer[ArcherUpgrade].Price;
        if (up == 1)
            return Knight[KnightUpgrade].Price;
        if (up == 2)
            return Mage[MageUpgrade].Price;
        if (up == 3)
            return wallManager.WallFaces[wallManager.CurrentUpgrade].Price;
        if (up == 4)
            return Torch[0].Price;
        return 0;
    }

    public int GetUpgradePrice(int up)
    {
        if (up == 0)
            return Archer[ArcherUpgrade + 1].Price;
        if (up == 1)
            return Knight[KnightUpgrade + 1].Price;
        if (up == 2)
            return Mage[MageUpgrade + 1].Price;
        if (up == 3)
            return wallManager.WallFaces[wallManager.CurrentUpgrade + 1].Price;
        return 0;
    }

    public bool CheckLastUpgrade(int up)
    {
        if (up == 0)
            return ArcherUpgrade == Archer.Length - 1;
        if (up == 1)
            return KnightUpgrade == Knight.Length - 1;
        if (up == 2)
            return MageUpgrade == Mage.Length - 1;
        if (up == 3)
            return wallManager.CurrentUpgrade == wallManager.WallFaces.Length - 1;
        return false;
    }

    public int UpgradeArcher()
    {
        ArcherUpgrade++;
        foreach (GameObject archer in GameObject.FindGameObjectsWithTag("Archer"))
        {
            archer.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Archer[ArcherUpgrade].sprite;
            archer.GetComponent<DestroyableEntity>().ChangeHP(Archer[ArcherUpgrade].Health);
            archer.GetComponent<Attackable>().Damage = Archer[ArcherUpgrade].HitPoint;
        }
        //player.WoodsCount -= Archer[ArcherUpgrade - 1].Price;
        return 0;
    }

    public int UpgradeKnight()
    {
        KnightUpgrade++;
        foreach (GameObject knight in GameObject.FindGameObjectsWithTag("Knight"))
        {
            knight.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Knight[KnightUpgrade].sprite;
            knight.GetComponent<DestroyableEntity>().ChangeHP(Knight[KnightUpgrade].Health);
            knight.GetComponent<Attackable>().Damage = Knight[KnightUpgrade].HitPoint;
            // Add chance to block attack
        }
        //player.RocksCount -= Knight[KnightUpgrade - 1].Price;
        return 1;
    }

    public int UpgradeMage()
    {
        MageUpgrade++;
        foreach (GameObject mage in GameObject.FindGameObjectsWithTag("Mage"))
        {
            mage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Mage[MageUpgrade].sprite;
            mage.GetComponent<DestroyableEntity>().ChangeHP(Mage[MageUpgrade].Health);
            mage.GetComponent<Attackable>().Damage = Mage[MageUpgrade].HitPoint;
            // add ability to heal more health
        }
        //player.RocksCount -= Mage[MageUpgrade - 1].Price;
        return 2;
    }

    public int UpgradeWall()
    {
        wallManager.CurrentUpgrade++;
        int currentUpgrade = wallManager.CurrentUpgrade;
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            wall.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wallManager.WallFaces[currentUpgrade].faces[wall.GetComponent<Walls>().facing];
            wall.GetComponent<DestroyableEntity>().ChangeHP(wallManager.WallFaces[currentUpgrade].Health);
        }
        //player.WoodsCount -= wallManager.WallFaces[wallManager.CurrentUpgrade].Price;
        return 3;
    }
}
