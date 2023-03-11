using JetBrains.Annotations;
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
    public GameObject bg;

    public List<Vector2> positions;

    float minDistance = 3f; // minimum distance between positions

    // Start is called before the first frame update
    void Start() {
        // Get the position (X,Y) of the sprite
        SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
        float height = spriteRenderer.sprite.bounds.size.y/2;
        float width = spriteRenderer.sprite.bounds.size.x/2;

        float posX = bg.transform.position.x - width;
        float posY = bg.transform.position.y - height;

        if (sprite.Count < planets)
        {
            planets = sprite.Count;
        }

        for (int i = 0; i < planets; i++)
        {
            randomPlanet = Random.Range(0, sprite.Count-1);

            GameObject planet = sprite[randomPlanet];

            Instantiate(planet);
            Vector2 newPosition;

            do
            {
                // generate a random Vector2 position within a certain range
                newPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            } while (IsTooCloseToExistingPositions(newPosition, positions, minDistance));

            planet.transform.position = newPosition;

            positions.Add(newPosition);

            sprite.RemoveAt(randomPlanet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsTooCloseToExistingPositions(Vector2 position, List<Vector2> existingPositions, float minDistance)
    {
        foreach (Vector2 existingPosition in existingPositions)
        {
            if (Vector2.Distance(existingPosition, position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }
}
