using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // the enemy prefab to spawn
    public Transform homePlanet; // the player's home planet
    public SpriteRenderer background; // the background sprite renderer
    public float spawnInterval = 5f; // how often to spawn enemies
    public float enemySpeed = 5f; // the speed at which enemies move
    Spaceship enemySpaceship;

    private float timeSinceLastSpawn = 0f;
    private float backgroundWidth;
    private float backgroundHeight;

    void Start()
    {
        // Get the dimensions of the background sprite
        backgroundWidth = background.bounds.size.x;
        backgroundHeight = background.bounds.size.y;

        SpawnEnemy();
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Determine a random side of the background to spawn the enemy on
        int side = Random.Range(0, 4); // 0=top, 1=right, 2=bottom, 3=left
        float x, y;

        switch (side)
        {
            case 0: // top
                x = Random.Range(-backgroundWidth / 2f, backgroundWidth / 2f);
                y = backgroundHeight / 2f;
                break;
            case 1: // right
                x = backgroundWidth / 2f;
                y = Random.Range(-backgroundHeight / 2f, backgroundHeight / 2f);
                break;
            case 2: // bottom
                x = Random.Range(-backgroundWidth / 2f, backgroundWidth / 2f);
                y = -backgroundHeight / 2f;
                break;
            case 3: // left
                x = -backgroundWidth / 2f;
                y = Random.Range(-backgroundHeight / 2f, backgroundHeight / 2f);
                break;
            default:
                x = 0f;
                y = 0f;
                break;
        }

        // Spawn the enemy at the chosen position and rotate it towards the player's home planet
        Vector3 spawnPosition = new Vector3(x, y, 0f);


        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Spaceship script = enemy.GetComponent<Spaceship>();
        script.isEnemy = true;



        Vector3 direction = homePlanet.position - enemy.transform.position;

        enemySpaceship = enemy.GetComponent<Spaceship>();


        enemySpaceship.moveOnTo(new Vector2(homePlanet.position.x + 0.001f, homePlanet.position.y + 0.001f), 0); // ofset beacuse tomasek
        //Instantiate(enemy);
    }
}
