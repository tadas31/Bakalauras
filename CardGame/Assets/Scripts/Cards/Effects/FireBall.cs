using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    private int damage;                     // Amount of damage spell deals
    private bool attacking;                 // If player pressed on card to attack true else false;

    // Start is called before the first frame update
    void Start()
    {
        damage = 3;
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

             // If player hovers over cards in hand spell returns to hand
            if (defendingCard != null && defendingCard.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveSpellBackToHand(gameObject);
                Destroy(defendingCard.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0) && !attacking)
        {
            attacking = true;
            StartCoroutine("castSpell");
        }
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator castSpell()
    {
        Transform defendingCard = attackHelper.getDefendingCard(null);

        if (defendingCard != null)
        {

            attackHelper.cachedLineRenderer.enabled = false;

            // Deals damage to defending card
            string[] defendingCardStats = defendingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text.Split('/');
            int defendingCardAttack = int.Parse(defendingCardStats[0]);
            int defendingCardLife = int.Parse(defendingCardStats[1]);
            defendingCardLife -= damage;

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = defendingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = "-" + damage;
            yield return new WaitForSeconds(0.3f);
            defendingCardDamageTaken.text = null;


            if (defendingCardLife < 1)
                Destroy(defendingCard.parent.gameObject);
            else
                defendingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text = defendingCardAttack + " / " + defendingCardLife;

            // Resets all booleans to allow attack with another card
            attacking = false;
            attackHelper.isAttacking = false;
            Destroy(gameObject);
        }
        else
        {
            attackHelper.moveSpellBackToHand(gameObject);
            attacking = false;
        }
    }
}
