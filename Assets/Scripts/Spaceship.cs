using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    bool isEnemy;
    Sprite sprite;
    float attack;
    float speed;
    public Spaceship(Upgrades upgrades, bool isEnemy)
    {
        this.isEnemy = isEnemy;
        //sprite = 
        attack = 10 * upgrades.attackAndSpeedUpgrades;
        speed = 5 * upgrades.defenceUpgrades;
    }
}