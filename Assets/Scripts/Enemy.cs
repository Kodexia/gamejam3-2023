using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyShipPrefab;  // the prefab for the enemy ship
    public Transform homePlanet;  // the transform of the home planet
    public float raycastDistance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random angle in radians
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate the direction vector for the raycast
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Cast a ray in the chosen direction
        RaycastHit2D hit = Physics2D.Raycast(homePlanet.position, direction, raycastDistance);

        // If the ray hits something, spawn an enemy ship at the hit point
        if (hit.collider != null)
        {
            Vector2 spawnPosition = hit.point;
            GameObject enemyShip = Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity);
            //EnemyMovement enemyMovement = enemyShip.GetComponent<EnemyMovement>();
            //if (enemyMovement != null)
            //{
            //    enemyMovement.target = homePlanet;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
