using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class Planet : MonoBehaviour
{
    public bool isTargeted = false;
    public bool isMined;
    public string name;
    private List<Resource> resources = new List<Resource>();
    public Resource ore;

    System.Random rnd = new System.Random();

    [SerializeField] public List<GameObject> spaceshipSprites = new List<GameObject>();
    [SerializeField] public GameObject homePlanet;
    [SerializeField] float timeToMine = 10;

    [SerializeField] GameObject progressBar;
    [SerializeField] Canvas progressBarCanvas;
    [SerializeField] Transform planetsPosition;
    GameObject planetsProgressBar;
    GameObject player;
    float timePassed;
    private GameObject collSpaceship;
    public Planet()
    {
        resources.Add(new Resource("Azurite", rnd.Next(100, 500)));
        resources.Add(new Resource("Crimtain", rnd.Next(100, 500)));
        resources.Add(new Resource("Uranium", rnd.Next(100, 500)));

        ore = resources[rnd.Next(0, resources.Count)];
        name = $"E-{rnd.Next(100, 500)}";

    }
    private void Start()
    {
        player = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        if (isMined)
        {
            timePassed += Time.deltaTime;
            MinePlanet();
            planetsProgressBar.GetComponent<ProgressBar>().progress = timePassed / timeToMine * 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("COLIDED WITH PLANET");
        //Debug.Log(isTargeted);
        //Debug.Log(tag) ;

        Spaceship spaceshipScript = other.gameObject.GetComponent<Spaceship>();

        bool isShipEnemy = spaceshipScript.isEnemy;
        Debug.Log("Is ship enenmy: " + isShipEnemy);

        if (isShipEnemy == true && tag == "homeplanet")
        {
            //Debug.Log("enemy ship collided!");
            SetAttack(spaceshipScript, other.gameObject);
            return;
        }

        if (isShipEnemy == false && tag == "homeplanet" && spaceshipScript.ore != null)
        {
            //Debug.Log("ship with ore");
            //Debug.Log(spaceshipScript.ore.amm);
            //Debug.Log(spaceshipScript.ore.name);

            foreach (var playerOre in player.GetComponent<Player>().resources)
            {
                if (playerOre.name == spaceshipScript.ore.name && spaceshipScript.ore.amm > 0)
                {
                    playerOre.amm += spaceshipScript.ore.amm;
                    spaceshipScript.ore.amm = 0;
                }
            }

            //Debug.Log("shipt desroy, pleeeaseeeee" + other.gameObject);
            Destroy(other.gameObject, 2f);

        }


        if (isTargeted == true && tag != "homeplanet")
        {
            planetsProgressBar = Instantiate(progressBar);
            planetsProgressBar.transform.position = new Vector2(planetsPosition.position.x, planetsPosition.position.y + 2);
            planetsProgressBar.transform.SetParent(progressBarCanvas.transform);

            spaceshipScript.ore = new Resource(ore.name, ore.amm);
            collSpaceship = other.gameObject;

            isTargeted = false;
            isMined = true;
        }
    }
    void MinePlanet()
    { 
        if (timePassed >= timeToMine - player.GetComponent<Player>().playerUpgrades.miningSpeedAndSpeedUpgrades)
        {
            timePassed = 0;
            isMined = false;
            Destroy(planetsProgressBar);

            collSpaceship.GetComponent<Spaceship>().moveOnTo(new Vector2(homePlanet.transform.position.x + 0.001f, homePlanet.transform.position.y + 0.001f)); // ofset beacuse tomasek neunmi programovat
            ore.amm = 0;
            //todo mining scaled by upgrades
            if (this.ore.amm <= 0)
            {
                Destroy(this.GameObject());
            }
        }
    }

    public void SetAttack(Spaceship spaceship, GameObject enemyShipObject)
    {
        Player playerScritp = GameObject.Find("Main Camera").GetComponent<Player>();
        playerScritp.EnemyAttack(spaceship, this, enemyShipObject);
    }
}


