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

    public bool attacking;                  // If card is selected to attack true else false.
    private bool selectingCardToAttack;     // If player is selecting card to attack true else false.

    public bool attacked
    {
        get { return _attacked; }
        set { _attacked = value; }
    }
    private bool _attacked;                 // If card has attacked true else false.

    private bool attackStarted;             // If defending card is found and attacking card started attacking true else false.
    private AttackHelper attackHelper;      // Reference to attack helper script.

    // True when card moves towards defending card.
    public bool moveForward
    {
        get { return _moveForward; }
    }
    private bool _moveForward;               

    // True when card goes back to it's position
    public bool moveBack                     
    {
        get { return _moveBack; }
    }
    private bool _moveBack;

    private Vector3 startPos;               // Cards starting position.

    private GameObject glow;                // Game object that adds glow to the card.

    Transform attackingCard;                // Transform of attacking card.
    Transform defending;                    // Transform of defending card.



    // Start is called before the first frame update
    public void Start()
    {
        speed = 8f;
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        glow = transform.GetChild(0).Find("Glow").gameObject;
        attacking = false;
        selectingCardToAttack = false;
        _attacked = true;
        attackStarted = false;

        _moveForward = false;
        _moveBack = false;
    }

    // Update is called once per frame
    public void Update()
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
                attackingCard = transform.GetChild(0);
                Transform defending = attackHelper.getDefendingCard(attackingCard);
                Transform defendingPlayer = attackHelper.getDefendingPlayer();

                //If it is in puzzle game mode.
                if (GameObject.Find("ClientManager") == null)
                {
                    if (defending != null)
                        StartCoroutine( attack(defending, "card") );
                    else if ( defendingPlayer != null)
                        StartCoroutine( attack(defendingPlayer, "player") );
                }
                //If it is classic game mode.
                else
                {
                    BoardCards.attackingCard = transform.GetSiblingIndex();
                    BoardCards.defendingCard = defending.GetSiblingIndex();
                    //If the card is attacking another card.
                    if (defending != null)
                        ClientSend.Attack(this.gameObject.name, defending.parent.gameObject.name);
                    //If the player is attacking player.
                    else if (defendingPlayer != null)
                    {
                        int id = defendingPlayer.parent.GetComponent<PlayerManager>().id;
                        ClientSend.Attack(this.gameObject.name, id.ToString());
                    }
                }

                // Cancel attack
                if (defending == null && defendingPlayer == null)
                {
                    attacking = false;
                    attackHelper.cachedLineRenderer.enabled = false;
                    attackHelper.isAttacking = false;
                }
                    
            }
            selectingCardToAttack = true;
        }

        movement();
        glow.SetActive(!_attacked);

    }


    /// <summary>
    /// Selects card on which is this script to attack
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_attacked && !attackHelper.isAttacking)
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

    public void AttackAnimationToPlayer(Health playerHealth)
    {
        IEnumerator corountine = attackAnimationToPlayer(playerHealth);
        StartCoroutine(corountine);
    }

    private IEnumerator attackAnimationToPlayer(Health playerHealth) 
    {
        startPos = this.transform.position;
        attackingCard = this.transform;
        //If the player health was taken.
        if (playerHealth.transform.name == "Player")
        {
            this.defending = GameObject.Find("Board/AttackPlayer/Player").transform;
        }
        //If enemy health was taken.
        else
        {
            this.defending = GameObject.Find("Board/AttackPlayer/Enemy").transform;
        }

        attackStarted = true;
        attackHelper.cachedLineRenderer.enabled = false;

        // Waits for attacking card to move to defending card.
        _moveForward = true;
        while (_moveForward)
            yield return null;

        CardStatsHelper attackingCardStats = GetComponent<CardStatsHelper>();
        playerHealth.takeDamage(attackingCardStats.getAttack());

        _moveBack = true;
        while (_moveBack)
        {
            yield return null;
        }
        // Marks card as attacked this turn
        _attacked = true;
        attackStarted = false;
    }

    public void AttackAnimationToCard(Transform defendingCard)
    {
        IEnumerator corountine = attackAnimation(defendingCard.GetChild(0));
        StartCoroutine(corountine);
    }

    private IEnumerator attackAnimation(Transform defendingCard)
    {
        Debug.Log("Animation started");
        // Deals and receives damage.
        if (defendingCard != null)
        {

            this.defending = defendingCard;
            startPos = this.transform.position;
            attackingCard = this.transform.GetChild(0);

            attackStarted = true;

            if (attackHelper != null)
            {
                attackHelper.cachedLineRenderer.enabled = false;
            }

            // Waits for attacking card to move to defending card.
            _moveForward = true;
            while (_moveForward)
                yield return null;

            // Gets card stats handlers
            CardStatsHelper attackingCardStats = GetComponent<CardStatsHelper>();
            CardStatsHelper defendingCardStats = defendingCard.GetComponentInParent<CardStatsHelper>();

            // Deals damage and updates life's
            attackingCardStats.takeDamage(defendingCardStats.getAttack());
            defendingCardStats.takeDamage(attackingCardStats.getAttack());


            TextMeshProUGUI attackingCardDamageTaken = attackingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI defendingCardDamageTaken = defendingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();

            attackingCardDamageTaken.text = "-" + defendingCardStats.getAttack();
            defendingCardDamageTaken.text = "-" + attackingCardStats.getAttack();

            yield return new WaitForSeconds(0.1f);

            // Waits for card to get back to it's position.
            _moveBack = true;
            while (_moveBack)
            {
                yield return null;
            }

            attackingCardDamageTaken.text = null;
            defendingCardDamageTaken.text = null;

            // Destroys dead cards
            attackingCardStats.checkIfSitllAlive();
            defendingCardStats.checkIfSitllAlive();

            // Marks card as attacked this turn
            _attacked = true;
            attackStarted = false;
        }
        attacking = false;
        selectingCardToAttack = false;

        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator attack(Transform defending, string defenderType)
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

        // Get defending card.
        this.defending = defending;

        // If enemy has taunts and selected card is not or selected target is player attack gets canceled
        if (hasTaunt && (this.defending.GetComponentInParent<Taunt>() == null || defenderType == "player"))
            canAttack = false;

        // Deals and receives damage.
        if (this.defending != null && canAttack)
        {

            startPos = attackingCard.position;

            attackStarted = true;
            attackHelper.cachedLineRenderer.enabled = false;

            // Waits for attacking card to move to defending card.
            _moveForward = true;
            while (_moveForward)
                yield return null;

            if (defenderType == "card")
            {
                // Gets card stats handlers
                CardStatsHelper attackingCardStats = GetComponent<CardStatsHelper>();
                CardStatsHelper defendingCardStats = this.defending.GetComponentInParent<CardStatsHelper>();

                // Deals damage and updates life's
                attackingCardStats.takeDamage(defendingCardStats.getAttack());
                defendingCardStats.takeDamage(attackingCardStats.getAttack());


                TextMeshProUGUI attackingCardDamageTaken = attackingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI defendingCardDamageTaken = this.defending.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();

                attackingCardDamageTaken.text = "-" + defendingCardStats.getAttack();
                defendingCardDamageTaken.text = "-" + attackingCardStats.getAttack();

                yield return new WaitForSeconds(0.1f);

                // Waits for card to get back to it's position.
                _moveBack = true;
                while (_moveBack)
                {
                    yield return null;
                }

                attackingCardDamageTaken.text = null;
                defendingCardDamageTaken.text = null;

                // Destroys dead cards
                attackingCardStats.checkIfSitllAlive();
                defendingCardStats.checkIfSitllAlive();
            }
            else
            {
                Health playerHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
                CardStatsHelper attackingCardStats = GetComponent<CardStatsHelper>();
                playerHealth.takeDamage(attackingCardStats.getAttack());

                _moveBack = true;
                while (_moveBack)
                {
                    yield return null;
                }
            }

            // Marks card as attacked this turn
            _attacked = true;
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
        if (_moveForward)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, defending.position, speed * Time.deltaTime);
            if (Vector3.Distance(attackingCard.position, defending.position) < 2f)
            {
                _moveForward = false;
            }
        }

        // Attacking card moves to it's starting place
        if (_moveBack)
        {
            attackingCard.position = Vector3.MoveTowards(attackingCard.position, startPos, speed * Time.deltaTime);
            if (Vector3.Distance(attackingCard.position, startPos) < 0.001f)
            {
                attackingCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                _moveBack = false;
            }
        }
    }
}
