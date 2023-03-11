using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string name { get; private set; }
    Sprite sprite { get; set; }
    

    public Resource(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

}
