using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public int attackUpgrades { get; private set; }
    public int speedUpgrades { get; private set; }
    public int miningSpeedUpgrades {get; private set;}
    public Upgrades()
    {
        attackUpgrades = 1;
        speedUpgrades = 1;
        miningSpeedUpgrades = 1;
    }
    public void upgradeAttack(List<Resource> useableOres)
    {
        foreach(Resource ore in useableOres)
        {
            if(ore.name == "naseore" && ore.amm >= 10 * attackUpgrades)
            {
                attackUpgrades++;
            }
        }
    }
    public void upgradeSpeed(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "naseore" && ore.amm >= 10 * speedUpgrades)
            {
                speedUpgrades++;
            }
        }
    }
    public void upgradeMiningSpeed(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "naseore" && ore.amm >= 10 * miningSpeedUpgrades)
            {
                miningSpeedUpgrades++;
            }
        }
    }
}
