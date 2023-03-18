using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform homePlanet;
    public SpriteRenderer background;
    public float spawnInterval = 5f;
    public float enemySpeed = 5f;
    Spaceship enemySpaceship;

    private float timeSinceLastSpawn = 0f;
    private float backgroundWidth;
    private float backgroundHeight;

    void Start()
    {
        // Get the dimensions of the background
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
        // Determine a random side of the background to spawn the enemy
        int side = Random.Range(0, 4);
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

        Vector3 spawnPosition = new Vector3(x, y, 0f);


        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Spaceship script = enemy.GetComponent<Spaceship>();
        script.isEnemy = true;
        script.speed = 10;

        enemySpaceship = enemy.GetComponent<Spaceship>();


        enemySpaceship.moveOnTo(new Vector2(homePlanet.position.x + 0.001f, homePlanet.position.y + 0.001f)); // ofset beacuse tomasek neunmi programovat
        //Instantiate(enemy);
    }
}
