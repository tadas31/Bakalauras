using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreMana : MonoBehaviour, IDescription, ISpellDamage
{

    private int manaToRestore;      // Amount of mana to restore.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Restores " + manaToRestore + " mana";
        return description;
    }

    // Sets amount of attack card will gain for each charge on board.
    public void setSpellDamage(int spellDamage)
    {
        manaToRestore = spellDamage;
    }
}
