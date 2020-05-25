using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCardWithSetAttackOrMore : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    private int minimumAttack;              // Minimum amount of attack card needs to have.
    private bool destroying;
    
    // Start is called before the first frame update
    void Start()
    {
        destroying = false;
    }

    // Executes when script is enabled
    public void OnEnable()
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
    public void OnDisable()
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

        if (!destroying)
        {
            Transform defendingCard = attackHelper.getDefendingCard(null);

            // If player hovers over cards in hand spell returns to hand
            if (defendingCard != null && defendingCard.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveCardBackToHand(gameObject);
                destroying = false;
                attackHelper.isAttacking = false;
                Destroy(defendingCard.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                destroying = true;
                if (defendingCard != null && defendingCard.position != new Vector3(2000, 2000, 2000))
                    castSpell(defendingCard);
                else
                {
                    attackHelper.moveCardBackToHand(gameObject);
                    destroying = false;
                    attackHelper.isAttacking = false;
                }
            }
        }
    }

    /// <summary>
    /// Destroys card.
    /// </summary>
    private void castSpell(Transform defendingCard)
    {
        attackHelper.cachedLineRenderer.enabled = false;

        if (defendingCard.GetComponentInParent<CardStatsHelper>().getAttack() >= minimumAttack)
            Destroy(defendingCard.parent.gameObject);
       
        attackHelper.isAttacking = false;
        Destroy(gameObject);

    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Destroys card that has " + minimumAttack + " or more attack";
        return description;
    }

    // Sets minimum amount of attack card can have
    public void setSpellDamage(int spellDamage)
    {
        minimumAttack = spellDamage;
    }
}
