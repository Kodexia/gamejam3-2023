using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Planet : MonoBehaviour
{
    public bool isTargeted = false;
    public string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    public Resource ore;
    Sprite sprite;
    
    System.Random rnd = new System.Random();

    [SerializeField]
    public List<GameObject> spaceshipSprites = new List<GameObject>();

    [SerializeField]
    public GameObject homePlanet;

    public Planet()
    {
        resources.Add(new Resource("Azurite", rnd.Next(10, 50)));
        resources.Add(new Resource("Crimtain", rnd.Next(10, 50)));
        resources.Add(new Resource("Uranium", rnd.Next(10, 50)));
        
        ore = resources[rnd.Next(0, resources.Count)];
        name = $"E-{rnd.Next(100,500)}";
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTargeted && tag != "homeplanet")
        {
            int type = other.GetComponent<Spaceship>().type;
            Debug.Log("Destroyed");
            Destroy(other.gameObject);
            MinePlanet(GameObject.Find("Main Camera Planets").GetComponent<Player>());
            GameObject player = GameObject.Find("Main Camera Planets");
            player.GetComponent<Player>().attack += 5;
            player.GetComponent<Player>().defence += 5;
            foreach(Resource ore in player.GetComponent<Player>().resources)
            {
                Debug.Log($"Name:{ore.name} Amount: {ore.amm}");
            }
            
        }
    }
    void MinePlanet(Player player)
    {
        foreach(var playerOre in player.resources)
        {
            if(playerOre.name == ore.name && ore.amm > 0) {
                if(ore.amm < 2*player.playerUpgrades.miningSpeedAndSpeedUpgrades)
                {
                    playerOre.amm += ore.amm;
                    ore.amm = 0;
                }
                else
                {
                    playerOre.amm += 2 * player.playerUpgrades.miningSpeedAndSpeedUpgrades;

                    ore.amm -= 2 * player.playerUpgrades.miningSpeedAndSpeedUpgrades;
                }

            }
        }
    }

}
