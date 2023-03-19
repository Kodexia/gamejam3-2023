using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    Player player;
    [SerializeField] public int attackDemage = 1;
    public bool isEnemy;
    public int type;
    [SerializeField] float attack;
    [SerializeField] public float speed;
    [SerializeField] public int storage;
    [SerializeField] GameObject progressBar;
    [SerializeField] Canvas progressBarCanvas;
    public Resource ore;

    public Vector2 whereToGo = new Vector2(0, 0);
    Upgrades upgrades;

    public Spaceship(bool isEnemy)
    {
        player = GameObject.Find("Main Camera").GetComponent<Player>();
        upgrades = new Upgrades(player);
        this.isEnemy = isEnemy;
        attack = 10 * upgrades.attackUpgrades;
        speed = 5 * upgrades.defenceUpgrades;
    }

    private void Start()
    {
        //if (isEnemy == true)
        //{
        //    var shipsProgressBar = Instantiate(progressBar);

        //    shipsProgressBar.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
        //    shipsProgressBar.transform.SetParent(progressBarCanvas.transform);
        //}
    }
    private void OnDestroy()
    {
        if (!isEnemy)
        {
            player = GameObject.Find("Main Camera").GetComponent<Player>();
            player.attack += 5;
            player.defence += 5;
        }
    }

    private void Update()
    {
        if (whereToGo != new Vector2(0, 0))
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
            transform.Translate(direction * Time.deltaTime * speed, Space.World);

            // Calculate the angle to the target position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object towards the target position only on the z-axis
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }

        //progressBar

        if (isEnemy == true)
        {

        }
    }

    public void moveOnTo(Vector2 toGo)
    {
        whereToGo = toGo;
    }
}