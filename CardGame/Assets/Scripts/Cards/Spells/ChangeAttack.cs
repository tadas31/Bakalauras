using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttack : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.
    private int newAttack;                  // New attack for selected card.

    private bool changingAttack;            // If player pressed on card to change it's attack true else false.

    // Start is called before the first frame update
    void Start()
    {
        changingAttack = false;
        
    }

    private void OnEnable()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

        // Enables arrow drawing and sets coordinates.
        attackHelper.arrowOrigin = new Vector3(0, -4, 6);
        attackHelper.arrowTarget = new Vector3(0, 0, 6);
        attackHelper.cachedLineRenderer.enabled = true;

        // Prevents another card from being used.
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

        if (!changingAttack)
        {
            Transform defendingCard = attackHelper.getDefendingCard(null);


            // If player hovers over cards in hand spell returns to hand
            if (defendingCard != null && defendingCard.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveCardBackToHand(gameObject);
                changingAttack = false;
                attackHelper.isAttacking = false;
                Destroy(defendingCard.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                changingAttack = true;
                if (defendingCard != null && defendingCard.position != new Vector3(2000, 2000, 2000))
                    castSpell(defendingCard);
                else
                {
                    attackHelper.moveCardBackToHand(gameObject);
                    changingAttack = false;
                    attackHelper.isAttacking = false;
                }
            }
        }
    }

    /// <summary>
    /// Set's new attack for selected card.
    /// </summary>
    /// <param name="defendingCard"></param>
    private void castSpell(Transform defendingCard)
    {
        attackHelper.cachedLineRenderer.enabled = false;

        // Set's new attack.
        defendingCard.GetComponentInParent<CardStatsHelper>().setAttack(newAttack);

        // Resets all booleans to allow attack with another card
        changingAttack = false;
        attackHelper.isAttacking = false;
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Changes card's attack to " + newAttack;
        return description;
    }

    // New attack for card.
    public void setSpellDamage(int spellDamage)
    {
        newAttack = spellDamage;
    }
}
