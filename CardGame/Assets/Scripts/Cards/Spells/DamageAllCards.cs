﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DamageAllCards : MonoBehaviour, IDescription, ISpellDamage
{

    private AttackHelper attackHelper;      // Reference to attack helper script.

    private int damage;                     // Amount of damage spell deals

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

        // Prevents another card to be selected to attack.
        attackHelper.isAttacking = true;

        StartCoroutine("castSpell");
    }

    /// <summary>
    /// Casts spell
    /// </summary>
    /// <returns></returns>
    private IEnumerator castSpell()
    {
        List<Transform> allCards = attackHelper.getAllEnemyCards().Concat(attackHelper.getAllPlayerCards()).ToList();

        foreach (Transform card in allCards)
        {
            card.GetComponentInParent<CardStatsHelper>().takeDamage(damage);

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = "-" + damage;
        }

        yield return new WaitForSeconds( attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS );

        foreach (Transform card in allCards)
        {
            card.GetComponentInParent<CardStatsHelper>().checkIfSitllAlive();

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = null;
        }

        attackHelper.isAttacking = false;
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Deals " + damage + " damage to all cards";
        return description;
    }

    // Sets amount of damage spell deals
    public void setSpellDamage(int spellDamage)
    {
        damage = spellDamage;
    }
}
