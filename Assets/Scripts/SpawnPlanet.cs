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
    public List<GameObject> planets;
    [SerializeField]
    public List<GameObject> asteroids;


    [SerializeField]
    public int spawnedPlanets = 8;

    [SerializeField]
    float distanceBetweenPoints = 10f;
    [SerializeField]
    int maxFailedChecks = 15;

    [SerializeField]
    public GameObject bg;

    [SerializeField]
    public GameObject homePlanet;

    public List<Vector2> positions;
    Dictionary<Vector2, GameObject> generatedPositions = new Dictionary<Vector2, GameObject>();

    // Problém je že mìnìní transformu nezmìní actually ten gameobject, a mnì už došly nápady :/

    // Start is called before the first frame update
    void Awake()
    {
        // Get the position (X,Y) of the sprite
        SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
        float height = spriteRenderer.sprite.bounds.size.y / 2;
        float width = spriteRenderer.sprite.bounds.size.x / 2;

        float posX = bg.transform.position.x - width;
        float posY = bg.transform.position.y - height;

        List<Vector2> vectors = GeneratePoints(distanceBetweenPoints, spriteRenderer.sprite.bounds.size);


        if (planets.Count < spawnedPlanets)
        {
            spawnedPlanets = planets.Count;
        }

        Instantiate(homePlanet);

        Vector2 homePlanetSpawnVector = new Vector2(posX, posY) + new Vector2(width, height);

        homePlanet.transform.SetPositionAndRotation(homePlanetSpawnVector, Quaternion.identity);
        homePlanet.name = "Home planet";
        positions.Add(homePlanetSpawnVector);

        vectors.RemoveAt(0);

        for (int i = 0; i < spawnedPlanets; i++)
        { 
            if(vectors.Count == 0)
            {
                break;
            }

            GameObject planet = planets[UnityEngine.Random.Range(0, planets.Count - 1)];

            Instantiate(planet);

            int chosenIndex = UnityEngine.Random.Range(0, vectors.Count);
            Vector2 chosenVector = vectors[chosenIndex];

            Debug.Log($"Planet {planet.name} spawned at {chosenVector + new Vector2(posX, posY)}");
            

            Vector2 spawnPosition = chosenVector + new Vector2(posX, posY);

            planet.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            Debug.Log("Transform pos: " + planet.transform.position);
            Debug.Log("Local transform: " + planet.transform.localPosition);
            positions.Add(spawnPosition);
            generatedPositions.Add(spawnPosition, planet);

            vectors.RemoveAt(chosenIndex);
        }

        while(vectors.Count > 0)
        {
            Vector2 spawnPosition = vectors[0] + new Vector2(posX, posY);
            vectors.RemoveAt(0);

            GameObject asteroid = asteroids[UnityEngine.Random.Range(0, asteroids.Count)];
            Instantiate(asteroid);
            asteroid.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

            positions.Add(spawnPosition);
        }
    }

    void LateUpdate()
    {
        for(int i = 0; i < generatedPositions.Count; i++)
        {
            Vector2 desiredPosition = generatedPositions.Keys.ToArrayPooled()[i];

            GameObject planet;
            generatedPositions.TryGetValue(desiredPosition, out planet);

            if (planet == null) continue;

            Vector2 currentPosition = planet.transform.position;
            if(desiredPosition != currentPosition)
            {
                Debug.Log("Fixing pos for " + planet.name);
                Debug.Log("Current pos: " + currentPosition);
                Debug.Log("Desired pos: " + desiredPosition);
                Debug.Log("--");
                planet.transform.position = new Vector2(desiredPosition.x, desiredPosition.y); // Tadyto prostì nezmìní pozici ani za hovno, idk proè
            }
        }
    }

    public List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        points.Add(sampleRegionSize / 2);
        grid[(int)(points[0].x / cellSize), (int)(points[0].y / cellSize)] = points.Count;

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

        // Final check
        for(int i = 0; i < points.Count; i++)
        {
            for(int j = 0; j < points.Count; j++)
            {
                if(i == j) continue;
                Vector2 father = points[i];
                Vector2 son = points[j];

                Debug.Log($"{radius} >= {Vector2.Distance(father, son)}");

                if(radius >= Vector2.Distance(father, son))
                {
                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        Debug.LogError("Found bugged spawn, removing father");
                        points.RemoveAt(i);
                    }
                    else
                    {
                        Debug.LogError("Found bugged spawn, removing son");
                        points.RemoveAt(j);
                    }
                }
            }
        }

        return points;
    }

    bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
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