using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    Resource ore;
    Sprite sprite;


    public Planet(string name)
    {
        resources.Add(new Resource("Azurite", Random.Range(10, 50)));
        resources.Add(new Resource("Crimtain", Random.Range(10, 50)));
        resources.Add(new Resource("Uranium", Random.Range(10, 50)));

        ore = resources[Random.Range(0, resources.Count)];
        this.name = name;
        
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
