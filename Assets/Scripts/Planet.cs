using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    string name { get; set; }
    Dictionary<Resource, int> layers = new Dictionary<Resource, int> { };
}
