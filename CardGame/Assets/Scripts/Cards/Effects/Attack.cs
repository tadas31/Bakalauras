using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class Attack : MonoBehaviour, IPointerClickHandler
{
    private float speed;                    // Card moving speed.

    private bool attacking;                 // If card is selected to attack true else false.
    private bool selectingCardToAttack;     // If player is selecting card to attack true else false.
    private bool attacked;                  // If card has attacked true else false.
    private bool attackStarted;             // If defending card is found and attacking card started attacking true else false.
    private DrawArrow drawArrow;            // Reference to draw arrow script.
    private GameManager gameManager;        // Reference to game manager script.

    private bool moveForward;               // True when card moves towards defending card.
    private bool moveBack;                  // True when card goes back to it's position
    private Vector3 startPos;               // Cards starting position.

    Transform attackingCard;                // Transform of attacking card.
    Transform defendingCard;                // Transform of defending card.

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.18f;
        drawArrow = transform.parent.parent.GetComponent<DrawArrow>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        attacking = false;
        selectingCardToAttack = false;
        attacked = false;
        attackStarted = false;

        moveForward = false;
        moveBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Attack card
        if (attacking )
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f; // Distance of the plane from the camera
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
            drawArrow.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);


            if (Input.GetMouseButtonUp(0) && selectingCardToAttack && !attackStarted)
            {
                StartCoroutine("attack");
            }
            selectingCardToAttack = true;
        }

        movement();

    }


    /// <summary>
    /// Selects card on which is this script to attack
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!attacked && !gameManager.isAttacking)
        {
            // Selects card for attacking.
            attacking = true;
            selectingCardToAttack = false;

            // Enables arrow drawing and sets coordinates.
            float xPos = transform.position.x < 0 ? transform.position.x / 1.14f : transform.position.x / 1.1f;
            float yPos = transform.position.y < 0 ? -0.6f : 3.45f;
            drawArrow.arrowOrigin = new Vector3(xPos, yPos, 6);
            drawArrow.arrowTarget = new Vector3(xPos, yPos, 6);
            drawArrow.cachedLineRenderer.enabled = true;

            // Prevents defending card from being selected for attacking.
            gameManager.isAttacking = true;
        }
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator attack()
    {
        // Raycasts all UI elements.
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Gets parent to all card components.
        attackingCard = transform.GetChild(0);
        startPos = attackingCard.position;

        // Defending card.
        defendingCard = null;

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
            attackStarted = true;
            drawArrow.cachedLineRenderer.enabled = false;

            // Waits for attacking card to move to defending card
            moveForward = true;
            while (moveForward)
                yield return null;

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

            TextMeshProUGUI attackingCardDamageTaken = attackingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI defendingCardDamageTaken = defendingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();

            attackingCardDamageTaken.text = "-" + defendingCardAttack;
            defendingCardDamageTaken.text = "-" + attackingCardAttack;

            yield return new WaitForSeconds(0.1f);

            // Waits for card to get back to it's position.
            moveBack = true;
            while (moveBack)
                yield return null;

            attackingCardDamageTaken.text = null;
            defendingCardDamageTaken.text = null;

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
            attackStarted = false;
        }

        // Resets all booleans to allow attack with another card
        attacking = false;
        selectingCardToAttack = false;

        drawArrow.cachedLineRenderer.enabled = false;
        gameManager.isAttacking = false;
    }

    /// <summary>
    /// Moves attacking card to defending card and back
    /// </summary>
    private void movement()
    {
        // Moves to attacking card to defending card.
        if (moveForward)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, defendingCard.position, speed);
            if (Vector3.Distance(attackingCard.position, defendingCard.position) < 2f)
            {
                moveForward = false;
            }
        }

        // Attacking card moves to it's starting place
        if (moveBack)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, startPos, speed);
            if (Vector3.Distance(attackingCard.position, startPos) < 0.001f)
            {
                attackingCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                moveBack = false;
            }
        }
    }
}
