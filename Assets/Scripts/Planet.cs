using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class Planet : MonoBehaviour
{
    public bool isTargeted = false;
    bool isMined;
    public string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    public Resource ore;

    System.Random rnd = new System.Random();

    [SerializeField]
    public List<GameObject> spaceshipSprites = new List<GameObject>();

    [SerializeField]
    public GameObject homePlanet;
    GameObject player;
    [SerializeField]
    float timeToMine = 10;
    float timePassed;

    [SerializeField]
    Vector3 planetPosition;
    GameObject spawned;
    public Planet()
    {
        resources.Add(new Resource("Azurite", rnd.Next(10, 50)));
        resources.Add(new Resource("Crimtain", rnd.Next(10, 50)));
        resources.Add(new Resource("Uranium", rnd.Next(10, 50)));

        ore = resources[rnd.Next(0, resources.Count)];
        name = $"E-{rnd.Next(100, 500)}";

    }
    private void Start()
    {
        player = GameObject.Find("Main Camera Planets");






    }
    private void Update()
    {
        if (isMined)
        {
            timePassed += Time.deltaTime;
            MinePlanet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("COLIDED WITH PLANET");
        //Debug.Log(isTargeted);
        //Debug.Log(tag) ;

        bool isShipEnemy = other.gameObject.GetComponent<Spaceship>().isEnemy; // throws an error if an asteroid collides with the planet --> fix spawner
        Debug.Log(isShipEnemy);

        if (isShipEnemy == true)
        {
            Debug.Log("enemy ship collided!");
            SetAttack(other.gameObject.GetComponent<Spaceship>(), other.gameObject);
            return;
        }


        //if (isTargeted == true && tag == "homeplanet")
        //{
        //    Debug.Log("Home planet");
        //    Destroy(other.gameObject);
        //    isTargeted = false;
        //}

        if (isTargeted == true && tag != "homeplanet")
        {
            int type = other.GetComponent<Spaceship>().type;
            Debug.Log(tag + "");
            Debug.Log("Destroyed");
            Destroy(other.gameObject);
            isTargeted = false;
            isMined = true;


        }





    }
    void MinePlanet()
    {

        if (timePassed >= timeToMine - player.GetComponent<Player>().playerUpgrades.miningSpeedAndSpeedUpgrades)
        {
            foreach (var playerOre in player.GetComponent<Player>().resources)
            {
                if (playerOre.name == ore.name && ore.amm > 0)
                {
                    playerOre.amm += ore.amm;
                    ore.amm = 0;



                }

                Debug.Log($"Name:{playerOre.name} Amount: {playerOre.amm}");
            }
            timePassed = 0;
            isMined = false;
            Destroy(spawned);
            player.GetComponent<Player>().attack += 5;
            player.GetComponent<Player>().defence += 5;
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


