using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;
using System;


public class Player : MonoBehaviour
{
    public bool isDead = false;
    public bool isWon= false;
    public int attack = 0;
    public int defence = 0;
    public int raidsSurvived = 0;


    public List<GameObject> spaceshipSprites = new List<GameObject>();
    public List<Resource> resources = new List<Resource>();
    public Upgrades playerUpgrades = new Upgrades();
    List<Spaceship> spaceshipsOnPlanet = new List<Spaceship>();
    List<Spaceship> spaceshipsOffPlanet = new List<Spaceship>();
    Spaceship mainSpaceship;
    public bool isAttacking = false;

    Vector2 pointOfTargetedPlanet;
    public Player()
    {

        resources.Add(new Resource("Azurite", 100));
        resources.Add(new Resource("Crimtain", 0));
        resources.Add(new Resource("Uranium", 0));
    }
    private void Start()
    {
        mainSpaceship = spaceshipSprites[0].GetComponent<Spaceship>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null && hit.collider.tag == "planet" && !isAttacking)
            {
                pointOfTargetedPlanet = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y) ;
                Debug.Log(pointOfTargetedPlanet);
                mainSpaceship.whereToGo = pointOfTargetedPlanet;
                Planet planet = hit.collider.GetComponent<Planet>();
                planet.isTargeted = true;
                Instantiate(spaceshipSprites[0]);

            }
        }
        
    }
    void CheckForEndGame()
    {
        if (this.isDead || this.isWon)
        {
            if (isDead)
                PlayerStats.isDead = true;


            PlayerStats.NumberOfAzurite = this.resources[0].amm;
            PlayerStats.NumberOfCrimtain = this.resources[1].amm;
            PlayerStats.NumberOfUranium = this.resources[2].amm;
            PlayerStats.attack = this.attack;
            PlayerStats.defense = this.defence;
            PlayerStats.raidSurvived = raidsSurvived;
            PlayerStats.endTime = DateTime.Now;



        }
    }
}
