using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowUpgradeMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI attackAndSpeedUpgradeText;
    [SerializeField]
    TextMeshProUGUI defenceUpgradeText;
    [SerializeField]
    TextMeshProUGUI miningSpeedUpgradeText;
    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        attackAndSpeedUpgradeText.text = $"Attack & Speed \n[{player.playerUpgrades.attackAndSpeedUpgrades}]";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
