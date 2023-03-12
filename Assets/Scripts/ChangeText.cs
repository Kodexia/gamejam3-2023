using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI Azurite;
    [SerializeField]
    TextMeshProUGUI Uranium;
    [SerializeField]
    TextMeshProUGUI Crimtain;
    [SerializeField]
    Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateText()
    {
        foreach(Resource ore in player.resources)
        {
            if(ore.name == "Azurite")
            {
                Azurite.text = ore.amm.ToString();
            }
            if (ore.name == "Crimtain")
            {
                Crimtain.text = ore.amm.ToString();
            }
            if (ore.name == "Uranium")
            {
                Uranium.text = ore.amm.ToString();
            }
        }
    }
}
