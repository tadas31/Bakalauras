using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Attack : MonoBehaviour, IPointerClickHandler
{
    private bool attacking;                         // If card is selected to attack true else false.
    private bool selectingCardToAttack;             // If player is selecting card to attack true else false
    private bool attacked;                          // If card has attacked true else false.

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        selectingCardToAttack = false;
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Attack card
        if (attacking)
        {
            if (Input.GetMouseButtonUp(0) && selectingCardToAttack)
            {
                attack();
            }
            selectingCardToAttack = true;
        }
    }


    /// <summary>
    /// Selects card on which is this script to attack
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!attacked)
        {
            attacking = true;
            selectingCardToAttack = false;
        }
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private void attack()
    {
        // Raycasts all UI elements.
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Gets parent to all card components.
        Transform attackingCard = transform.GetChild(0);

        // Defending card.
        Transform defendingCard = null;

        // Gets defending card.
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name.ToLower().Contains("background - ") && result.gameObject.GetComponentInParent<Canvas>().name.ToLower().Contains("board") && attackingCard != result.gameObject.transform)
            {
                defendingCard = result.gameObject.transform;
                break;
            }
        }

        // Deals and receives damage
        if (defendingCard != null)
        {
            // Gets attacking card stats.
            string[] attackingCardStats = attackingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text.Split('/');
            int attackingCardAttack = int.Parse(attackingCardStats[0]);
            int attackingCardLife = int.Parse(attackingCardStats[1]);

            // Gets defending card stats.
            string[] defendingCardStats = defendingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text.Split('/');
            int defendingCardAttack = int.Parse(defendingCardStats[0]);
            int defendingCardLife = int.Parse(defendingCardStats[1]);

            // Deals and takes damage.
            attackingCardLife -= defendingCardAttack;
            defendingCardLife -= attackingCardAttack;

            // Destroys dead cards and updates life values
            if (attackingCardLife < 1)
                Destroy(attackingCard.parent.gameObject);
            else
                attackingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text = attackingCardAttack + " / " + attackingCardLife;

            if (defendingCardLife < 1)
                Destroy(defendingCard.parent.gameObject);
            else
                defendingCard.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text = defendingCardAttack + " / " + defendingCardLife;

            // Marks card as attacked this turn
            attacked = true;
        }

        attacking = false;
        selectingCardToAttack = false;
    }
}
