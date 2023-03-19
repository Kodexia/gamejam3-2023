using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public bool isDead = false;
    public bool isWon = false;
    public int attack = 10;
    public int defence = 10;
    public int raidsSurvived = 0;
    public int minedPlanet = 0;
    public bool isAttacking = false;

    public int attackCount = 0;
    private float nextAttackTime = 0;
    private int enemyAttack = 0;
    public bool isUnderAttack = false;
    public int maxAttackRepeats = 5;
    public float attackTime = 0;
    private Planet homePlanet;



    [SerializeField] TextMeshPro attackDefenceText;

    [SerializeField] List<GameObject> spaceshipSprites = new List<GameObject>();
    [SerializeField] public List<Resource> resources = new List<Resource>();
    public Upgrades playerUpgrades;
    Spaceship mainSpaceship;

    Vector2 pointOfTargetedPlanet;

    private GameObject enemyShip;

    public Player()
    {
        playerUpgrades = new Upgrades(this);
        PlayerStats.startTime = DateTime.Now;
        resources.Add(new Resource("Azurite", 0));
        resources.Add(new Resource("Crimtain", 10));
        resources.Add(new Resource("Uranium", 10));
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
            //Debug.Log("is targeted: "+hit.collider.GetComponent<Planet>().isTargeted);

            if (hit.collider != null && hit.collider.tag == "planet" && attack >= 5 && defence >= 5 && hit.collider.GetComponent<Planet>().isTargeted == false && hit.collider.GetComponent<Planet>().isMined == false)
            {
                pointOfTargetedPlanet = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                //Debug.Log(pointOfTargetedPlanet);
                if ((int)(this.playerUpgrades.miningSpeedAndSpeedUpgrades / 3) < 3)
                {
                    mainSpaceship = spaceshipSprites[(int)this.playerUpgrades.miningSpeedAndSpeedUpgrades / 3].GetComponent<Spaceship>();
                }
                else
                {
                    mainSpaceship = spaceshipSprites[2].GetComponent<Spaceship>();
                }
                mainSpaceship.whereToGo = pointOfTargetedPlanet;
                Planet planet = hit.collider.GetComponent<Planet>();
                planet.isTargeted = true;
                Instantiate(mainSpaceship);
                attack -= 5;
                defence -= 5;
            }
        }

        if (minedPlanet == 7)
        {
            this.isWon = true;
            CheckForEndGame();
        }

        UpdateAttack();

    }
    public void CheckForEndGame()
    {
        if (this.isDead || this.isWon)
        {


            PlayerStats.isDead = this.isDead;
            PlayerStats.isWon = this.isWon;
            PlayerStats.NumberOfAzurite = this.resources[0].amm;
            PlayerStats.NumberOfCrimtain = this.resources[1].amm;
            PlayerStats.NumberOfUranium = this.resources[2].amm;
            PlayerStats.attack = this.attack;
            PlayerStats.defense = this.defence;
            PlayerStats.raidSurvived = raidsSurvived;
            PlayerStats.endTime = DateTime.Now;

            SceneManager.LoadScene(5);

        }
    }

    public void EnemyAttack(Spaceship spaceship, Planet planet, GameObject enemyShipObject)
    {
        homePlanet = planet;
        attackCount = 0;
        isUnderAttack = true;
        enemyAttack = spaceship.attackDemage;
        nextAttackTime = Time.time + 1;

        enemyShip = enemyShipObject;

    }

    void UpdateAttack()
    {
        if (isUnderAttack == true)
        {
            attackTime += Time.deltaTime;
        }
        if (Time.time >= nextAttackTime && attackCount < maxAttackRepeats && isUnderAttack == true)
        {
            // Attack
            Debug.Log("Attacking!");


            this.defence -= (int)(enemyAttack * 1.5f);
            this.attack -= enemyAttack;

            if (defence <= 0 || attack <= 0)
            {
                this.isDead = true;
            }

            this.CheckForEndGame();

            if (attackDefenceText != null) // temp gui
            {
                // Update the attackDefenseText to show the new attack and defense values
                attackDefenceText.text = "Attack: " + this.attack + "\nDefense: " + this.defence;

            }

            attackCount++;
            nextAttackTime = Time.time + 1;

        }
        else if (attackCount == maxAttackRepeats && isUnderAttack == true)
        {
            this.isUnderAttack = false;
            attackCount = 0;
            attackTime = 0;
            Debug.Log("Destroyed ship, correct!");
            Destroy(enemyShip);
            homePlanet.DestroyProgressBar();

            raidsSurvived++;
        }
    }
}
