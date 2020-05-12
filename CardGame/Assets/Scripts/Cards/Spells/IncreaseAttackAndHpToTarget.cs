using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseAttackAndHpToTarget : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    private bool upgrading;                 // If player pressed on card to upgrade.
    private int increaseBy;                 // Amount of health and attack to add.

    // Start is called before the first frame update
    void Start()
    {
        upgrading = false;
    }

    private void OnEnable()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

        // Enables arrow drawing and sets coordinates.
        attackHelper.arrowOrigin = new Vector3(0, -4, 6);
        attackHelper.arrowTarget = new Vector3(0, 0, 6);
        attackHelper.cachedLineRenderer.enabled = true;

        // Prevents defending cards from being selected for attacking.
        attackHelper.isAttacking = true;
    }

    private void OnDisable()
    {
        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;      // Distance of the plane form the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);

        if (!upgrading)
        {
            Transform cardToUpgrade = attackHelper.getDefendingCard(null);

            // If player hovers over cards in hand spell returns to hand
            if (cardToUpgrade != null && cardToUpgrade.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveCardBackToHand(gameObject);
                Destroy(cardToUpgrade.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0) && !upgrading)
        {
            upgrading = true;
            StartCoroutine( castSpell() );
        }
    }

    private IEnumerator castSpell()
    {
        Transform cardToUpgrade = attackHelper.getDefendingCard(null);
        if (cardToUpgrade != null)
        {
            attackHelper.cachedLineRenderer.enabled = false;

            CardStatsHelper cardToUpgradeStarts = cardToUpgrade.GetComponentInParent<CardStatsHelper>();

            // Upgrades card
            cardToUpgradeStarts.takeDamage( -increaseBy );
            cardToUpgradeStarts.setAttack(cardToUpgradeStarts.getAttack() + increaseBy);

            TextMeshProUGUI cardToUpgradeIncreasedBy = cardToUpgrade.transform.Find("Image/AttackHealthRecieved").GetComponent<TextMeshProUGUI>();
            cardToUpgradeIncreasedBy.text = "+" + increaseBy + "/" + increaseBy;
            yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
            cardToUpgradeIncreasedBy.text = null;

            // Resets all booleans and destroys spell card
            upgrading = false;
            attackHelper.isAttacking = false;
            Destroy(gameObject);
        }
        else
        {
            attackHelper.moveCardBackToHand(gameObject);
            upgrading = false;
        }

        
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Increase health and attack of target by " + increaseBy;
        return description;
    }

    // Sets amount of damage spell deals
    public void setSpellDamage(int spellDamage)
    {
        increaseBy = spellDamage;
    }
}
