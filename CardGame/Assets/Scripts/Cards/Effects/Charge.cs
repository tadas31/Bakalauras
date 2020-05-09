using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour, IDescription
{

    private Attack attack;      // Reference to attack script

    // Start is called before the first frame update
    void Start()
    {
        attack = gameObject.GetComponent<Attack>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.attacked)
        {
            attack.attacked = false;
            enabled = false;
        }

    }

    public string getDescription()
    {
        return "Charge";
    }
}