using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    string name { get; set; }
    Dictionary<Resource, int> layers;
    Sprite sprite;


    public Planet(string name, Dictionary<Resource,int> layers, Sprite sprite)
    {
        
        this.name = name;
        this.layers = layers;
        this.sprite = sprite;
    }

}
