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
    public int attack = 10;
    public int defence = 10;
    public int raidsSurvived = 0;


    public List<GameObject> spaceshipSprites = new List<GameObject>();
    [SerializeField]
    public List<Resource> resources = new List<Resource>();
    public Upgrades playerUpgrades = new Upgrades();
    List<Spaceship> spaceshipsOnPlanet = new List<Spaceship>();
    List<Spaceship> spaceshipsOffPlanet = new List<Spaceship>();
    Spaceship mainSpaceship;
    public bool isAttacking = false;

    Vector2 pointOfTargetedPlanet;




    private int attackCount = 0;
    private float nextAttackTime = 0;
    private int enemyAttack = 0;
    public bool isUnderAttack = false;

    private GameObject enemyShip;

    public float attackIntervalMin = 1f; // minimum time between attacks
    public float attackIntervalMax = 2f; // maximum time between attacks
    public int maxAttackRepeats = 5; // maximum number of times to attack


    public Player()
    {

        resources.Add(new Resource("Azurite", 100));
        resources.Add(new Resource("Crimtain", 100));
        resources.Add(new Resource("Uranium", 100));
    }
    private void Start()
    {
        mainSpaceship = spaceshipSprites[(int)this.playerUpgrades.miningSpeedAndSpeedUpgrades / 3].GetComponent<Spaceship>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null && hit.collider.tag == "planet" && attack >= 5 && defence >= 5)
            {
                pointOfTargetedPlanet = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                Debug.Log(pointOfTargetedPlanet);
                mainSpaceship.whereToGo = pointOfTargetedPlanet;
                Planet planet = hit.collider.GetComponent<Planet>();
                planet.isTargeted = true;
                Instantiate(spaceshipSprites[(int)this.playerUpgrades.miningSpeedAndSpeedUpgrades / 3]);
                attack -= 5;
                defence -= 5;
            }
        }

        UpdateAttack();

    }
    public void CheckForEndGame()
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

    public void EnemyAttack(Spaceship spaceship, Planet planet, GameObject enemyShipObject)
    {
        attackCount = 0;
        isUnderAttack = true;
        enemyAttack = spaceship.enemyAttack;
        nextAttackTime = Time.time + UnityEngine.Random.Range(attackIntervalMin, attackIntervalMax);

        enemyShip = enemyShipObject;

    }

    void UpdateAttack()
    {
        if (Time.time >= nextAttackTime && attackCount < maxAttackRepeats && isUnderAttack == true)
        {
            // Attack
            Debug.Log("Attacking!");


            this.defence -= enemyAttack * 2;
            this.attack -= enemyAttack;
            if (defence <= 0)
            {
                this.isDead = true;
            }
            this.CheckForEndGame();

            // Increase the attack count
            attackCount++;

            // Set the time for the next attack
            nextAttackTime = Time.time + (UnityEngine.Random.Range(attackIntervalMin, attackIntervalMax));

        }else if (attackCount == maxAttackRepeats && isUnderAttack == true)
        {
            this.isUnderAttack = false;
            attackCount = 0;
            Debug.Log("GG");
            Destroy(enemyShip);
            raidsSurvived++;

        }
    }
}
