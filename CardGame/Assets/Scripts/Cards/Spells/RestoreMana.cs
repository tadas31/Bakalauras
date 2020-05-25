using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreMana : MonoBehaviour, IDescription, ISpellDamage
{
    private int manaToRestore;              // Amount of mana to restore.
    
    // Start is called before the first frame update
    void Start()
    {
        // Prevents another card from being used.
        GameObject.Find("Board").GetComponent<AttackHelper>().isAttacking = true;

        GameObject.Find("Canvas/Player").GetComponent<Mana>().IncreaseMana(manaToRestore);
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Restores " + manaToRestore + " mana";
        return description;
    }

    // Sets amount of mana card will restore.
    public void setSpellDamage(int spellDamage)
    {
        manaToRestore = spellDamage;
    }
}
