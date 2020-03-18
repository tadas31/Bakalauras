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
    private AttackHelper attackHelper;      // Reference to attack helper script.

    private bool moveForward;               // True when card moves towards defending card.
    private bool moveBack;                  // True when card goes back to it's position
    private Vector3 startPos;               // Cards starting position.

    Transform attackingCard;                // Transform of attacking card.
    Transform defendingCard;                // Transform of defending card.

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.18f;
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
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
            attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);


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
        if (!attacked && !attackHelper.isAttacking)
        {
            // Selects card for attacking.
            attacking = true;
            selectingCardToAttack = false;

            // Enables arrow drawing and sets coordinates.
            float xPos = transform.position.x < 0 ? transform.position.x / 1.14f : transform.position.x / 1.1f;
            float yPos = transform.position.y < 0 ? -0.6f : 3.45f;
            attackHelper.arrowOrigin = new Vector3(xPos, yPos, 6);
            attackHelper.arrowTarget = new Vector3(xPos, yPos, 6);
            attackHelper.cachedLineRenderer.enabled = true;

            // Prevents defending card from being selected for attacking.
            attackHelper.isAttacking = true;
        }
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator attack()
    {
        List<Transform> allEnemyCards = attackHelper.getAllEnemyCards();

        bool hasTaunt = false;      // If true enemy has taunt
        bool canAttack = true;

        // Checks if enemy has taunts
        foreach (Transform enemyCard in allEnemyCards)
        {
            if (enemyCard.GetComponentInParent<Taunt>() != null)
            {
                hasTaunt = true;
                break;
            }
        }

        attackingCard = transform.GetChild(0);

        // Get defending card.
        defendingCard = attackHelper.getDefendingCard(attackingCard);

        // If enemy has taunts and selected card is not attack gets canceled
        if (hasTaunt && defendingCard.GetComponentInParent<Taunt>() == null)
            canAttack = false;

        // Deals and receives damage.
        if (defendingCard != null && canAttack)
        {
            startPos = attackingCard.position;

            attackStarted = true;
            attackHelper.cachedLineRenderer.enabled = false;

            // Waits for attacking card to move to defending card.
            moveForward = true;
            while (moveForward)
                yield return null;

            // Gets card stats handlers
            CardStatsHelper attackingCardStats = GetComponent<CardStatsHelper>();
            CardStatsHelper defendingCardStats = defendingCard.GetComponentInParent<CardStatsHelper>();

            // Deals damage and updates life's
            attackingCardStats.takeDamage( defendingCardStats.getAttack() );
            defendingCardStats.takeDamage( attackingCardStats.getAttack() );


            TextMeshProUGUI attackingCardDamageTaken = attackingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI defendingCardDamageTaken = defendingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();

            attackingCardDamageTaken.text = "-" + defendingCardStats.getAttack();
            defendingCardDamageTaken.text = "-" + attackingCardStats.getAttack();

            yield return new WaitForSeconds(0.1f);
            
            // Waits for card to get back to it's position.
            moveBack = true;
            while (moveBack)
            {
                yield return null;
            }

            attackingCardDamageTaken.text = null;
            defendingCardDamageTaken.text = null;

            // Destroys dead cards
            attackingCardStats.checkIfSitllAlive();
            defendingCardStats.checkIfSitllAlive();

            // Marks card as attacked this turn
            attacked = true;
            attackStarted = false;
        }

        // Resets all booleans to allow attack with another card
        attacking = false;
        selectingCardToAttack = false;

        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
    }

    /// <summary>
    /// Moves attacking card to defending card and back
    /// </summary>
    private void movement()
    {
        // Moves to attacking card to defending card.
        if (moveForward)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, defendingCard.position, speed * Time.deltaTime);
            if (Vector3.Distance(attackingCard.position, defendingCard.position) < 2f)
            {
                moveForward = false;
            }
        }

        // Attacking card moves to it's starting place
        if (moveBack)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, startPos, speed * Time.deltaTime);
            if (Vector3.Distance(attackingCard.position, startPos) < 0.001f)
            {
                attackingCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                moveBack = false;
            }
        }
    }
}
