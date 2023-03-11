using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Planet homePlanet;
    public List<Resource> resources = new List<Resource>();
    public Upgrades playerUpgrades = new Upgrades();
    List<Spaceship> spaceshipsOnPlanet = new List<Spaceship>();
    List<Spaceship> spaceshipsOffPlanet = new List<Spaceship>();
    public Player(Planet homePlanet)
    {
        this.homePlanet = homePlanet;
    }
    public void buySpaceship()
    {
        foreach(Resource ore in resources)
        {
            if(ore.name == "" && ore.amm > 1)
            {
                spaceshipsOnPlanet.Add(new Spaceship(playerUpgrades,false));
            }
        }
        
    }
    public void AttackPlanet(int ammOfSentUnits)
    {
        for (int i = 0; i < ammOfSentUnits; i++)
        {
            spaceshipsOffPlanet.Add(spaceshipsOnPlanet[i]);
            spaceshipsOnPlanet.RemoveAt(i);

        }
    }
    
}
