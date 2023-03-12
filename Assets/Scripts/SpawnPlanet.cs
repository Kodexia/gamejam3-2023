using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        List<Vector2> vectors = GeneratePoints(distanceBetweenPoints, spriteRenderer.sprite.bounds.size);

        if (sprite.Count < planets)
        {
            planets = sprite.Count;
        }

        for (int i = 0; i < planets; i++)
        {
            randomPlanet = UnityEngine.Random.Range(0, sprite.Count - 1);

            GameObject planet = sprite[randomPlanet];

            Instantiate(planet);

            Vector2 chosenVector = vectors[UnityEngine.Random.Range(0, vectors.Count)];

            Vector2 spawnPosition = new Vector2(posX + chosenVector.x, posY + chosenVector.y);

            planet.transform.position = spawnPosition;
            positions.Add(spawnPosition);
        }

        Instantiate(homePlanet);
        homePlanet.transform.position = new Vector2(0, 0);
        positions.Add(new Vector2(0, 0));
    }

    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(sampleRegionSize / 2);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for (int i = 0; i < numSamplesBeforeRejection; i++)
            {
                float angle = UnityEngine.Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * UnityEngine.Random.Range(radius, 2 * radius);
                if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }
            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }

        }

        return points;
    }

    static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            for (int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                        if (sqrDst < radius * radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}