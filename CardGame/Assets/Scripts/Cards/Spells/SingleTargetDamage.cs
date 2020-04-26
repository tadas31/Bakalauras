using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleTargetDamage : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    private int damage;                     // Amount of damage spell deals.
    private bool attacking;                 // If player pressed on card to attack true else false.

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
    }

    // Executes when script is enabled
    private void OnEnable()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        // Enables arrow drawing and sets coordinates.
        attackHelper.arrowOrigin = new Vector3(0, -4, 6);
        attackHelper.arrowTarget = new Vector3(0, 0, 6);
        attackHelper.cachedLineRenderer.enabled = true;

        // Prevents defending card from being selected for attacking.
        attackHelper.isAttacking = true;
    }

    // Executes when script is disabled
    private void OnDisable()
    {
        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
    }


    // Update is called once per frame
    void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; // Distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);

        if (!attacking)
        {
            Transform defendingCard = attackHelper.getDefendingCard(null);
            Transform defendingPlayer = attackHelper.getDefendingPlayer();

            // If player hovers over cards in hand spell returns to hand
            if (defendingCard != null && defendingCard.position == new Vector3(2000, 2000, 2000) && defendingPlayer == null)
            {
                attackHelper.moveCardBackToHand(gameObject);
                Destroy(defendingCard.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                attacking = true;
                if (defendingCard != null && defendingCard.position != new Vector3(2000, 2000, 2000))
                    StartCoroutine(castSpell(defendingCard, "card") );
                else if (defendingPlayer != null)
                    StartCoroutine(castSpell(defendingPlayer, "player"));
            }
        }

       
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator castSpell(Transform defending, string defenderType)
    {
        //Transform defendingCard = attackHelper.getDefendingCard(null);

        if (defending != null)
        {
            attackHelper.cachedLineRenderer.enabled = false;

            if (defenderType == "card")
            {
                CardStatsHelper defendingCardStats = defending.GetComponentInParent<CardStatsHelper>();

                // Deals damage to defending card
                defendingCardStats.takeDamage(damage);

                // Displays damage dealt to defending card
                TextMeshProUGUI defendingCardDamageTaken = defending.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
                defendingCardDamageTaken.text = "-" + damage;
                yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
                defendingCardDamageTaken.text = null;

                // Destroys card if its dead
                defendingCardStats.checkIfSitllAlive();
            }
            else
            {
                Health playerHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
                playerHealth.takeDamage(damage);
            }

            // Resets all booleans to allow attack with another card
            attacking = false;
            attackHelper.isAttacking = false;
            Destroy(gameObject);

        }
        else
        {
            attackHelper.moveCardBackToHand(gameObject);
            attacking = false;
        }
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "deals " + damage + " damage to one enemy card";
        return description;
    }

    // Sets amount of damage spell deals
    public void setSpellDamage(int spellDamage)
    {
        damage = spellDamage;
    }
}
