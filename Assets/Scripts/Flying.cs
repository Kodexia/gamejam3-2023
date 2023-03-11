using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Flying : MonoBehaviour
{

    [SerializeField]
    public Vector2 whereToGo = new Vector2(10, 10);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveOnTo(whereToGo);
    }

    public void moveOnTo(Vector2 toGo)
    {
        /*Vector2 distance = new Vector2(transform.position.x , transform.position.y);*/
        if (transform.position.y >= toGo.y - 0.1 && transform.position.y <= toGo.y + 0.1 && transform.position.x >= toGo.x - 0.1 && transform.position.x <= toGo.x + 0.1)
        {
            transform.position = new Vector2(toGo.x, toGo.y);
        }

        // Calculate the direction to the target position
        Vector3 direction = (toGo - (Vector2)transform.position).normalized;

        // Move the object towards the target position
        transform.Translate(direction * Time.deltaTime, Space.World);

        // Calculate the angle to the target position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object towards the target position only on the z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
