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
        Debug.Log(isTargeted);
        if (isTargeted == true && tag == "homeplanet")
        {
            Debug.Log("Home planet");
            Destroy(other.gameObject);
            isTargeted = false;
        }
        
        if (isTargeted && tag != "homeplanet")
        {
            int type = other.GetComponent<Spaceship>().type;
            Debug.Log("Destroyed");
            Destroy(other.gameObject);
            if(MinePlanet(GameObject.Find("Main Camera Planets").GetComponent<Player>()))
            {
                isTargeted = false;
                GameObject newPrefab = Instantiate(spaceshipSprites[type], transform.position, Quaternion.identity);
                newPrefab.GetComponent<Spaceship>().whereToGo = new Vector2(0.1f, 0.1f);
                homePlanet.GetComponent<Planet>().isTargeted = true;
            }
        }
    }
    bool MinePlanet(Player player)
    {
        foreach(var playerOre in player.resources)
        {
            Debug.Log("Started mining");
            if(playerOre.name == ore.name && ore.amm > 0) {
                Debug.Log("Started mining");
                if (ore.amm < 2*player.playerUpgrades.miningSpeedUpgrades)
                {
                    playerOre.amm += ore.amm;
                    ore.amm = 0;
                    Debug.Log("Done");
                    return true;
                }
                else
                {
                    playerOre.amm += 2 * player.playerUpgrades.miningSpeedUpgrades;
                    Debug.Log("Done");
                    ore.amm -= 2 * player.playerUpgrades.miningSpeedUpgrades;
                    return true;
                }

            }
        }
        return false;
    }

}
