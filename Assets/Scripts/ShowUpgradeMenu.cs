using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowUpgradeMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI miningSpeedAndSpeedUpgradeText;
    [SerializeField]
    TextMeshProUGUI defenceUpgradeText;
    [SerializeField]
    TextMeshProUGUI attackUpgradeText;
    [SerializeField]
    TextMeshProUGUI AzuriteText; 
    [SerializeField]
    TextMeshProUGUI UraniumText; 
    [SerializeField]
    TextMeshProUGUI CrimtainText;
    [SerializeField]
    Player player;
    [SerializeField]
    Canvas upgradeMenu;
    [SerializeField]
    TextMeshProUGUI notificationText;
    // Start is called before the first frame update
    void Start()
    {
        miningSpeedAndSpeedUpgradeText.text = $"Mining & Ship Speed \n[{player.playerUpgrades.miningSpeedAndSpeedUpgrades}]";
        defenceUpgradeText.text = $"Defence \n[{player.playerUpgrades.defenceUpgrades}]";
        attackUpgradeText.text = $"Attack \n[{player.playerUpgrades.attackUpgrades}]";

        AzuriteText.text = $"Need Azurite: [{player.playerUpgrades.miningSpeedAndSpeedUpgrades * 10}]";
        UraniumText.text = $"Need Uranium: [{player.playerUpgrades.defenceUpgrades * 10}]";
        CrimtainText.text = $"Need Crimtain: [{player.playerUpgrades.attackUpgrades * 10}]";
        upgradeMenu.enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Key works");
            if (upgradeMenu.enabled)
            upgradeMenu.enabled = false;
            else
            upgradeMenu.enabled = true;
            
        }
    }
    public void UpgradeSpeedAndMiningSpeed()
    {
        Debug.Log("Button up a&s Funguje");

        foreach(Resource ore in player.resources)
        {
            Debug.Log(ore.name + " " + ore.amm);
            if (ore.name == "Azurite" && ore.amm >= player.playerUpgrades.defenceUpgrades * 10)
            {
                player.playerUpgrades.upgradeSpeeds(player.resources);
                notificationText.text = "";

            }
            else if (ore.name == "Azurite" && ore.amm < player.playerUpgrades.defenceUpgrades * 10)
            {
                player.playerUpgrades.upgradeSpeeds(player.resources);
                notificationText.text = "Not Enough ore";
                Debug.Log("Not Enough ore");
            }
            Debug.Log(ore.name + " " + ore.amm);
        }
        miningSpeedAndSpeedUpgradeText.text = $"Mining & Ship Speed \n[{player.playerUpgrades.miningSpeedAndSpeedUpgrades}]";
        AzuriteText.text = $"Need Azurite: [{player.playerUpgrades.miningSpeedAndSpeedUpgrades * 10}]";
    }
    public void UpgradeDefence()
    {
        Debug.Log("Button up def Funguje");
        

        foreach (Resource ore in player.resources)
        {
            Debug.Log(ore.name + " " + ore.amm);
            if (ore.name == "Uranium" && ore.amm >= player.playerUpgrades.defenceUpgrades * 10)
            {
                player.playerUpgrades.upgradeDefence(player.resources);
                notificationText.text = "";

            }
            else if(ore.name == "Uranium" && ore.amm < player.playerUpgrades.defenceUpgrades * 10)
            {
                player.playerUpgrades.upgradeDefence(player.resources);
                notificationText.text = "Not Enough ore";
                Debug.Log("Not Enough ore");
            }
            Debug.Log(ore.name + " " + ore.amm);
        }
        defenceUpgradeText.text = $"Defence \n[{player.playerUpgrades.defenceUpgrades}]";
        UraniumText.text = $"Need Uranium: [{player.playerUpgrades.defenceUpgrades * 10}]";
    }
    public void UpgradeAttack()
    {
        Debug.Log("Button up mine Funguje");
        
        foreach (Resource ore in player.resources)
        {
            Debug.Log(ore.name + " " + ore.amm);
            if (ore.name == "Crimtain" && ore.amm >= player.playerUpgrades.defenceUpgrades * 10)
            {
                player.playerUpgrades.upgradeAttack(player.resources);
                notificationText.text = "";

            }
            else if (ore.name == "Crimtain" && ore.amm < player.playerUpgrades.defenceUpgrades * 10)
            {
               player.playerUpgrades.upgradeAttack(player.resources);
                notificationText.text = "Not Enough ore";
                Debug.Log("Not Enough ore");
            }
            Debug.Log(ore.name + " " + ore.amm);
        }
        attackUpgradeText.text = $"Attack \n[{player.playerUpgrades.attackUpgrades}]";
        CrimtainText.text = $"Need Crimtain: [{player.playerUpgrades.attackUpgrades * 10}]";
    }
}
