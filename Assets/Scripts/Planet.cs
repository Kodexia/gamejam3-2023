using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Planet : MonoBehaviour
{
    public bool isTargeted = false;
    bool isMined;
    public string name { get; set; }
    private List<Resource> resources = new List<Resource>();
    public Resource ore;
    Sprite sprite;
    
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
    GameObject progressBar;
    [SerializeField]
    Vector3 planetPosition;
    GameObject spawned;
    public Planet()
    {
        resources.Add(new Resource("Azurite", rnd.Next(10, 50)));
        resources.Add(new Resource("Crimtain", rnd.Next(10, 50)));
        resources.Add(new Resource("Uranium", rnd.Next(10, 50)));
        
        ore = resources[rnd.Next(0, resources.Count)];
        name = $"E-{rnd.Next(100,500)}";
        
    }
    private void Start()
    {
        player = GameObject.Find("Main Camera Planets");
        
        progressBar.GetComponentInParent<Canvas>().enabled = true;
        progressBar.transform.SetParent(progressBar.GetComponentInParent<Transform>());
        progressBar.transform.parent = GameObject.Find("Canvas").GetComponent<Transform>();




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
        if (isTargeted)
        {
            int type = other.GetComponent<Spaceship>().type;
            Debug.Log("Destroyed");
            Destroy(other.gameObject);
            isTargeted = false;
            isMined = true;
            spawned = Instantiate(progressBar, new Vector3(this.GetComponentInParent<Transform>().position.x, this.GetComponentInParent<Transform>().position.y + 2, this.GetComponentInParent<Transform>().position.z), Quaternion.identity);

            spawned.name = "PlanetMineBar";
            spawned.transform.SetParent(progressBar.GetComponentInParent<Canvas>().transform);
            spawned.transform.localScale = new Vector3(108, 108, 108);

        }
       
     
        
            

    }
    void MinePlanet()
    {
        spawned.GetComponent<ProgressBar>().progress = timePassed / timeToMine * 100f;
        if(timePassed >= timeToMine - player.GetComponent<Player>().playerUpgrades.miningSpeedAndSpeedUpgrades)
        {
            foreach (var playerOre in player.GetComponent<Player>().resources)
            {
                if (playerOre.name == ore.name && ore.amm > 0)
                {
                    if (ore.amm < 2 * player.GetComponent<Player>().playerUpgrades.miningSpeedAndSpeedUpgrades)
                    {
                        playerOre.amm += ore.amm;
                        ore.amm = 0;
                    }


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

}
