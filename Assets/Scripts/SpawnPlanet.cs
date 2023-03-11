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

    float minDistance = 4f; // minimum distance between positions

    // Start is called before the first frame update
    void Start()
    {
        // Get the position (X,Y) of the sprite
        SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
        float height = spriteRenderer.sprite.bounds.size.y / 2;
        float width = spriteRenderer.sprite.bounds.size.x / 2;

        float posX = bg.transform.position.x - width;
        float posY = bg.transform.position.y - height;

        if (sprite.Count < planets)
        {
            planets = sprite.Count;
        }

        for (int i = 0; i < planets; i++)
        {
            randomPlanet = Random.Range(0, sprite.Count - 1);

            GameObject planet = sprite[randomPlanet];

            Instantiate(planet);

            bool validPosition = false;
            Vector2 newPosition = Vector2.zero;
            while (!validPosition)
            {
                float planetX = Random.Range(posX + 3f, width - 3f);
                float planetY = Random.Range(posY + 3f, height - 3f);

                newPosition = new Vector2(planetX, planetY);

                validPosition = true;
                foreach (Vector2 existingPosition in positions)
                {
                    if (Vector2.Distance(existingPosition, newPosition) < minDistance)
                    {
                        Debug.Log($"{Vector2.Distance(existingPosition, newPosition)} | {minDistance}");
                        validPosition = false;
                        break;
                    }
                }
            }

            planet.transform.position = newPosition;

            positions.Add(newPosition);

            sprite.RemoveAt(randomPlanet);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
