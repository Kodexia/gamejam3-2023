using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class EndGameHandler : MonoBehaviour
{
    [SerializeField] TMP_Text Heading;
    [SerializeField] TMP_Text Stats;
    TimeSpan timeResult;

    void Start()
    {
        PlayerStats.endTime = DateTime.Now;
        if (PlayerStats.startTime != null)
        {
            timeResult = PlayerStats.endTime - PlayerStats.startTime;
        }
        else
        {
            timeResult = new TimeSpan(0, 0, 0);
        }
        if (PlayerStats.isDead)
        {
            Heading.text = "Game Over";
        }
        if (PlayerStats.isWon)
        {
            Heading.text = "You Won";
        }

        Stats.text = $"Attack : {PlayerStats.attack}\n"
            + $"Defense : {PlayerStats.defense}\n"
            + $"Raids Survived : {PlayerStats.raidSurvived}\n" +
            $"Quantity of Uranium : {PlayerStats.NumberOfUranium}\n" +
            $"Quantity of Azurite : {PlayerStats.NumberOfAzurite}\n" +
            $"Quantity of Crimtain : {PlayerStats.NumberOfCrimtain}\n" +
            "Time Spent : " + timeResult.Hours + "h " + timeResult.Minutes + "min " + timeResult.Seconds + "s";

    }
}
