using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string name { get; private set; }
    public int amm;
    
  
    

    public Resource(string name, int amm)
    {
        this.name = name;
        this.amm = amm;
    }

}
