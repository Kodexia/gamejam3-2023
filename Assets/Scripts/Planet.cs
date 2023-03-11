using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class Planet : MonoBehaviour
{
    public string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    public Resource ore;
    Sprite sprite;
    System.Random rnd = new System.Random();

    public Planet()
    {
        resources.Add(new Resource("Azurite", rnd.Next(10, 50)));
        resources.Add(new Resource("Crimtain", rnd.Next(10, 50)));
        resources.Add(new Resource("Uranium", rnd.Next(10, 50)));
        
        ore = resources[rnd.Next(0, resources.Count)];
        name = $"E-{rnd.Next(100,500)}";
        
    }

    void MinePlanet(Player player)
    {
        foreach(var playerOre in player.resources)
        {
            if(playerOre.name == ore.name) {
                playerOre.amm += 2 * player.playerUpgrades.miningSpeedUpgrades;
                ore.amm -= 2 * player.playerUpgrades.miningSpeedUpgrades;
            }
        }
    }

}
