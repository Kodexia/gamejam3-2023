using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public int miningSpeedAndSpeedUpgrades { get; private set; }
    public int defenceUpgrades { get; private set; }
    public int attackUpgrades { get; private set; }
    public Upgrades()
    {
        miningSpeedAndSpeedUpgrades = 1;
        defenceUpgrades = 1;
        attackUpgrades = 1;
    }
    public void upgradeSpeeds(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "Azurite" && ore.amm >= 10 * miningSpeedAndSpeedUpgrades)
            {
                ore.amm -= 10 * miningSpeedAndSpeedUpgrades;
                miningSpeedAndSpeedUpgrades++;
                Debug.Log("speed");

            }
        }
    }
    public void upgradeDefence(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "Uranium" && ore.amm >= 10 * defenceUpgrades)
            {
                ore.amm -= 10 * defenceUpgrades;
                defenceUpgrades++;
                Debug.Log("def");
            }
        }
    }
    public void upgradeAttack(List<Resource> useableOres)
    {
        foreach (Resource ore in useableOres)
        {
            if (ore.name == "Crimtain" && ore.amm >= 10 * attackUpgrades)
            {
                ore.amm -= 10 * attackUpgrades;
                attackUpgrades++;
                Debug.Log("attack");

            }
        }
    }
}
