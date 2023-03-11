using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    Planet homePlanet;
    List<Resource> resources = new List<Resource>();
    Upgrades playerUpgrades = new Upgrades();
    List<Spaceship> spaceships = new List<Spaceship>();
    public Player(Planet homePlanet)
    {
        this.homePlanet = homePlanet;
    }
    public void buySpaceship()
    {
        foreach(Resource ore in resources)
        {

        }
        
    }
    
}
