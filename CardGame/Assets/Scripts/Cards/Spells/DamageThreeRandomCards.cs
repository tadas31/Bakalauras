using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageThreeRandomCards : MonoBehaviour, IDescription, ISpellDamage
{

    private AttackHelper attackHelper;      // Reference to attack helper script.

    private int damage;                     // Amount of damage spell deals to each card.

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

        // Prevents another card to be selected to attack.
        attackHelper.isAttacking = true;

        StartCoroutine(castSpell());
    }

    /// <summary>
    /// Casts spell
    /// </summary>
    /// <returns></returns>
    private IEnumerator castSpell()
    {
        List<Transform> enemyCards = attackHelper.getAllEnemyCards();

        if (enemyCards.Count > 0)
        {
            List<Transform> cardsToAttack = new List<Transform>();

            // Picks random enemy cards.
            for (int i = 0; i < 3; i++)
            {
                cardsToAttack.Add(enemyCards[Random.Range(0, enemyCards.Count)]);
            }

            foreach (Transform card in cardsToAttack)
            {
                card.GetComponentInParent<CardStatsHelper>().takeDamage(damage);

                // Displays damage dealt to defending card
                TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image/DamageTaken").GetComponent<TextMeshProUGUI>();
                defendingCardDamageTaken.text = "-" + damage;
            }

            yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);

            foreach (Transform card in cardsToAttack)
            {
                card.GetComponentInParent<CardStatsHelper>().checkIfSitllAlive();

                // Displays damage dealt to defending card
                TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image/DamageTaken").GetComponent<TextMeshProUGUI>();
                defendingCardDamageTaken.text = null;
            }
        }
        attackHelper.isAttacking = false;
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Deals " + damage + " damage to 3 random enemy cards";
        return description;
    }

    // Sets amount of damage spell deals to each card.
    public void setSpellDamage(int spellDamage)
    {
        damage = spellDamage;
    }
}
