using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyRandomCard : MonoBehaviour, IDescription
{
    // Start is called before the first frame update
    void Start()
    {
        AttackHelper attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

        // Prevents another card to be selected to attack.
        attackHelper.isAttacking = true;

        List<Transform> allCards = attackHelper.getAllEnemyCards().Concat(attackHelper.getAllPlayerCards()).ToList();

        if (allCards.Count > 0)
        {
            // Picks random card.
            Transform cardToDestroy = allCards[Random.Range(0, allCards.Count)].parent;
            // Destroys card.
            Destroy(cardToDestroy.gameObject);
        }
      

        attackHelper.isAttacking = false;
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Destroys one random card";
        return description;
    }
}
