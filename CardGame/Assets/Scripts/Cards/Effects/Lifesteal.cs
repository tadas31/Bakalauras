using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : MonoBehaviour, IDescription
{
    private Attack attack;          // Reference to attack script.
    private bool lifesteal;         // True if effect is active.

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        lifesteal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.moveBack && !lifesteal)
        {
            lifesteal = true;
            // Determines if player or his enemy owns the card
            Transform cardParrent = transform.parent;
            Health health = null;
            if (cardParrent.name.ToLower().Contains("player"))
                health = GameObject.Find("Canvas/Player").GetComponent<Health>();
            else if (cardParrent.name.ToLower().Contains("enemy"))
                health = GameObject.Find("Canvas/Enemy").GetComponent<Health>();

            // Restores health
            if (health != null)
            {
                health.restoreHealth( GetComponent<CardStatsHelper>().getAttack() );
            }
        }

        if (!attack.moveBack && lifesteal)
            lifesteal = false;

    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Lifesteal";
        return description;
    }
}
