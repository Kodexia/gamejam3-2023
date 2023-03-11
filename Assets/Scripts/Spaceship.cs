using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship
{
    bool isEnemy;
    Sprite sprite;
    float attack;
    float speed;
    public Spaceship(Upgrades upgrades, Sprite sprite, bool isEnemy)
    {
        this.isEnemy = isEnemy;
        this.sprite = sprite;
        attack = 10 * upgrades.attackAndSpeedUpgrades;
        speed = 5 * upgrades.defenceUpgrades;
    }
}