using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcaneExplosion : MonoBehaviour
{

    private AttackHelper attackHelper;          // Reference to attack helper script.

    private int damage;                         // Amount of damage spell deals

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        damage = 1;

        // Prevents another card to be selected to attack.
        attackHelper.isAttacking = true;

        StartCoroutine("castSpell");
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    /// <summary>
    /// Casts spell
    /// </summary>
    /// <returns></returns>
    private IEnumerator castSpell()
    {

        List<Transform> enemyCards = attackHelper.getAllEnemyCards();

        foreach (Transform card in enemyCards)
        {
            card.GetComponentInParent<CardStatsHelper>().takeDamage(damage);

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = "-" + damage;
        }

        yield return new WaitForSeconds( attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS );

        foreach (Transform card in enemyCards)
        {
            card.GetComponentInParent<CardStatsHelper>().checkIfSitllAlive();

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = card.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = null;
        }

        attackHelper.isAttacking = false;
        Destroy(gameObject);
        
    }
}
