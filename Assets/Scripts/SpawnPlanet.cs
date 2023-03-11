using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnPlanet : MonoBehaviour
{
    

    [SerializeField]
    public List<GameObject> sprite;

    [SerializeField]
    public int planets = 8;
    [SerializeField]
    public int randomPlanet = 0;

    [SerializeField]
    float distanceBetweenPoints = 10f;
    [SerializeField]
    int maxFailedChecks = 15;

    [SerializeField]
    public GameObject bg;

    [SerializeField]
    public GameObject homePlanet;

    float minDistance = 4f; // minimum distance between positions
    public List<Vector2> positions;

    // Start is called before the first frame update
    void Start()
    {
        // Get the position (X,Y) of the sprite
        SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
        float height = spriteRenderer.sprite.bounds.size.y / 2;
        float width = spriteRenderer.sprite.bounds.size.x / 2;

        float posX = bg.transform.position.x - width;
        float posY = bg.transform.position.y - height;

        float sqrt2 = Mathf.Sqrt(2);
        float cellSize = distanceBetweenPoints / sqrt2;
        int dim = (int)Mathf.Ceil(width / cellSize);
        float[,] array = new float[dim, dim];
        //    X, Y
        Debug.Log("Dim: " + dim);
        Debug.Log("Cellsize: " + cellSize);
        Debug.Log("--");

        if (sprite.Count < planets)
        {
            planets = sprite.Count;
        }

        for (int i = 0; i < planets; i++)
        {
            randomPlanet = UnityEngine.Random.Range(0, sprite.Count - 1);

            GameObject planet = sprite[randomPlanet];

            Instantiate(planet);

            bool validPosition = false;
            Vector2 newPosition = Vector2.zero;
            int timesFailed = 0;
            while (!validPosition)
            {
                if (timesFailed >= maxFailedChecks) break;
                float planetX = UnityEngine.Random.Range(posX + 3f, width - 3f);
                float planetY = UnityEngine.Random.Range(posY + 3f, height - 3f);

                newPosition = new Vector2(planetX, planetY);

                validPosition = true;
                int xCellPosition = (int)(Math.Abs(planetX) / cellSize);
                int yCellPosition = (int)(Math.Abs(planetY) / cellSize);

                Debug.Log("X: " + xCellPosition + ", Y: " + yCellPosition);

                if (xCellPosition >= dim || yCellPosition >= dim)
                {
                    validPosition = false;
                    continue;
                }

                if (array[xCellPosition, yCellPosition] != 0 ||
                    (xCellPosition + 1 < dim && array[xCellPosition + 1, yCellPosition] != 0) ||
                    (xCellPosition - 1 >= 0 && array[xCellPosition - 1, yCellPosition] != 0) ||
                    (yCellPosition + 1 < dim && array[xCellPosition, yCellPosition + 1] != 0) ||
                    (yCellPosition - 1 >= 0 && array[xCellPosition, yCellPosition - 1] != 0))
                {
                    validPosition = false;
                    Debug.Log($"Check failed at cell {xCellPosition}, {yCellPosition}");
                    timesFailed++;
                }
                else
                {
                    array[xCellPosition, yCellPosition] = 1;
                }
            }
            if (timesFailed >= maxFailedChecks) continue;

            planet.transform.position = newPosition;

            positions.Add(newPosition);

            sprite.RemoveAt(randomPlanet);
        }

        Instantiate(homePlanet);
        homePlanet.transform.position = new Vector2(0, 0);
        positions.Add(new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}