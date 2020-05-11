using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnDeathRestoreHealth : MonoBehaviour, IDescription, ISpellDamage
{
   
    private bool restoring;                 // True if health is being restored to player.
    private int amountToRestore;            // Amount of hp to restore.

    // Start is called before the first frame update
    public void Start()
    {
        restoring = false;
    }


    private void OnDestroy()
    {
        // Determines if player or his enemy owns the card
        Transform cardParrent = transform.parent;
        Health health = null;
        if (cardParrent.name.ToLower().Contains("player"))
            health = GameObject.Find("Canvas/Player").GetComponent<Health>();
        else if (cardParrent.name.ToLower().Contains("enemy"))
            health = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
           
        // Restores health
        if (health != null && !restoring)
        {
            health.restoreHealth( amountToRestore );
        }
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "On deaths restores " + amountToRestore + " health to cards owner";
        return description;
    }

    // Sets amount of damage spell deals
    public void setSpellDamage(int spellDamage)
    {
        amountToRestore = spellDamage;
    }
}
