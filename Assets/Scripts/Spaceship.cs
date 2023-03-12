using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    bool isEnemy;
    Sprite sprite;
    float attack;
    float speed;
    [SerializeField]
    public Vector2 whereToGo = new Vector2(0, 0);
    [SerializeField]
    public int type = 0;
    
    public Spaceship(Upgrades upgrades, bool isEnemy)
    {
        this.isEnemy = isEnemy;
        //sprite = 
        attack = 10 * upgrades.attackAndSpeedUpgrades;
        speed = 5 * upgrades.defenceUpgrades;
    }

    private void Update()
    {
        if(whereToGo != new Vector2(0, 0))
        {
            /*Vector2 distance = new Vector2(transform.position.x , transform.position.y);*/
            if (transform.position.y >= whereToGo.y - 0.1 && transform.position.y <= whereToGo.y + 0.1 && transform.position.x >= whereToGo.x - 0.1 && transform.position.x <= whereToGo.x + 0.1)
            {
                transform.position = new Vector2(whereToGo.x, whereToGo.y);
                whereToGo = new Vector2(0, 0);
            }

            // Calculate the direction to the target position
            Vector3 direction = (whereToGo - (Vector2)transform.position).normalized;

            // Move the object towards the target position
            transform.Translate(direction * Time.deltaTime, Space.World);

            // Calculate the angle to the target position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object towards the target position only on the z-axis
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

    public void moveOnTo(Vector2 toGo, int type)
    {
        whereToGo = toGo;
        this.type = type;
    }
}