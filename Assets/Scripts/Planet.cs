using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class Planet : MonoBehaviour
{
    public bool isTargeted = false;
    public string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    public Resource ore;

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
        name = $"E-{rnd.Next(100, 500)}";

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        //Debug.Log("COLIDED WITH PLANET");
        //Debug.Log(isTargeted);
        //Debug.Log(tag) ;

        bool isShipEnemy = other.gameObject.GetComponent<Spaceship>().isEnemy; // throws an error if an asteroid collides with the planet --> fix spawner
        Debug.Log(isShipEnemy);

        if (isShipEnemy == true)
        {
            Debug.Log("enemy ship collided!");
            SetAttack(other.gameObject.GetComponent<Spaceship>(), other.gameObject);
        }


        if (isTargeted == true && tag == "homeplanet")
        {
            Debug.Log("Home planet");
            Destroy(other.gameObject);
            isTargeted = false;
        }

        if (isTargeted == true && tag != "homeplanet")
=======
        if (isTargeted && tag != "homeplanet")
>>>>>>> 765ba3508dd2fe90588e7910a2ae61e360779752
        {
            int type = other.GetComponent<Spaceship>().type;
            Debug.Log(tag + "");
            Debug.Log("Destroyed");
            Destroy(other.gameObject);
<<<<<<< HEAD
            if (MinePlanet(GameObject.Find("Main Camera Planets").GetComponent<Player>()))
=======
            MinePlanet(GameObject.Find("Main Camera Planets").GetComponent<Player>());
            GameObject player = GameObject.Find("Main Camera Planets");
            player.GetComponent<Player>().attack += 5;
            player.GetComponent<Player>().defence += 5;
            foreach(Resource ore in player.GetComponent<Player>().resources)
>>>>>>> 765ba3508dd2fe90588e7910a2ae61e360779752
            {
                Debug.Log($"Name:{ore.name} Amount: {ore.amm}");
            }
            
        }
    }
    void MinePlanet(Player player)
    {
        foreach (var playerOre in player.resources)
        {
<<<<<<< HEAD
            Debug.Log("Started mining");
            if (playerOre.name == ore.name && ore.amm > 0)
            {
                if (ore.amm < 2 * player.playerUpgrades.miningSpeedAndSpeedUpgrades)
=======
            if(playerOre.name == ore.name && ore.amm > 0) {
                if(ore.amm < 2*player.playerUpgrades.miningSpeedAndSpeedUpgrades)
>>>>>>> 765ba3508dd2fe90588e7910a2ae61e360779752
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

    public void AttackPlanet(Spaceship spaceship)
    {
        int damage = spaceship.enemyAttack;

        Player playerScritp = GameObject.Find("Main Camera Planets").GetComponent<Player>();

        playerScritp.defence -= damage * 2;
        playerScritp.attack -= damage;

        playerScritp.CheckForEndGame();
    }


    public void SetAttack(Spaceship spaceship, GameObject enemyShipObject)
    {
        Player playerScritp = GameObject.Find("Main Camera Planets").GetComponent<Player>();
        playerScritp.EnemyAttack(spaceship, this, enemyShipObject);
    }


}


