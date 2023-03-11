using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public int attackAndSpeedUpgrades { get; private set; }
    public int defenceUpgrades { get; private set; }
    public int miningSpeedUpgrades {get; private set;}
    public Upgrades()
    {
        attackAndSpeedUpgrades = 1;
        defenceUpgrades = 1;
        miningSpeedUpgrades = 1;
    }
    public void upgradeAttack(List<Resource> useableOres)
    {
        foreach(Resource ore in useableOres)
        {
            if(ore.name == "naseore" && ore.amm >= 10 * attackAndSpeedUpgrades)
            {
                attackAndSpeedUpgrades++;
            }
        }
    }
    public void upgradeSpeed(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "naseore" && ore.amm >= 10 * defenceUpgrades)
            {
                defenceUpgrades++;
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
