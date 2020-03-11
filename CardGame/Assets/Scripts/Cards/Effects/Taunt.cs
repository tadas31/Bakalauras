using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour, IDescription
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Taunt";
        return description;
    }
}
