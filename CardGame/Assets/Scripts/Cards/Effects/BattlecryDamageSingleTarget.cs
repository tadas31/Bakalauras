using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattlecryDamageSingleTarget : MonoBehaviour, IDescription, ISpellDamage
{

    private AttackHelper attackHelper;      // Reference to attack helper.

    private int damage;                     // Amount of damage to deal.
    private bool attacking;                 // Prevents battlecry from activating multiple times.


    public void OnEnable()
    {
        if (transform.parent != null)
        {
            attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

            // Enables arrow drawing and sets coordinates.
            float xPos = transform.position.x < 0 ? transform.position.x / 1.14f : transform.position.x / 1.1f;
            float yPos = transform.position.y < 0 ? -0.6f : 3.45f;
            attackHelper.arrowOrigin = new Vector3(xPos, yPos, 6);
            attackHelper.arrowTarget = new Vector3(xPos, yPos, 6);
            attackHelper.cachedLineRenderer.enabled = true;

            // Prevents another card to be selected to attack
            attackHelper.isAttacking = true;

            attacking = false;
        }
           
    }

    public void OnDisable()
    {
        if (transform.parent != null)
        {
            attackHelper.cachedLineRenderer.enabled = false;
            attackHelper.isAttacking = false;
            attacking = false;
        }
       
    }

    // Update is called once per frame
    public void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; // Distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);

        if (Input.GetMouseButtonUp(0) && !attacking)
        {
            Transform defendingCard = attackHelper.getDefendingCard(transform.GetChild(0));
            Transform defendingPlayer = attackHelper.getDefendingPlayer();

            if (defendingCard != null)
            {
                StartCoroutine( battlecry(defendingCard, "card") );
                attacking = true;
            }
            else if (defendingPlayer != null)
            {
                StartCoroutine( battlecry(defendingPlayer, "player") );
                attacking = true;
            }
            
        }
           
    }

    /// <summary>
    /// Deals damage.
    /// </summary>
    /// <returns></returns>
    private IEnumerator battlecry(Transform defending, string defenderType)
    {
        attackHelper.cachedLineRenderer.enabled = false;
        if (defenderType == "card")
        {
            CardStatsHelper defendingCardStats = defending.GetComponentInParent<CardStatsHelper>();
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

        enabled = false;
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Battlecry deals " + damage + " damage";
        return description;
    }

    // Sets amount of damage spell deals
    public void setSpellDamage(int spellDamage)
    {
        damage = spellDamage;
    }
}
