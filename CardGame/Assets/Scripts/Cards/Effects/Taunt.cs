using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour, IDescription
{
    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Taunt";
        return description;
    }
}
