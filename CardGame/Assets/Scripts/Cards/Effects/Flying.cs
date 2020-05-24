using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour, IDescription
{
    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Flying";
        return description;
    }
}
