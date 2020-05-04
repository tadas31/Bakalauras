using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour, IDescription
{
    private AttackHelper attackHelper;  // Reference to attack helper script.
    private bool resetingAttack;        // If player pressed on card to upgrade.

    // Start is called before the first frame update
    void Start()
    {
        resetingAttack = false;
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

        if (!resetingAttack)
        {
            Transform cardToResetAttack = attackHelper.getDefendingCard(null);

            // If player hovers over cards in hand spell returns to hand
            if (cardToResetAttack != null && cardToResetAttack.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveCardBackToHand(gameObject);
                Destroy(cardToResetAttack.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                resetingAttack = true;
                castSpell(cardToResetAttack);
            }
        }
    }

    /// <summary>
    /// Casts spell
    /// </summary>
    /// <param name="cardToResetAttack"></param>
    private void castSpell(Transform cardToResetAttack)
    {
        if (cardToResetAttack != null)
        {
            attackHelper.cachedLineRenderer.enabled = false;

            // Reset attack
            cardToResetAttack.transform.parent.GetComponent<Attack>().attacked = false;

            // Resets all booleans and destroys spell card
            resetingAttack = false;
            attackHelper.isAttacking = false;
            Destroy(gameObject);
        }
        else
        {
            attackHelper.moveCardBackToHand(gameObject);
            resetingAttack = false;
        }
    }

    public string getDescription()
    {
        return "Allows card that has already attacked attack again";
    }
}
