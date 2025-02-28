﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackHelper : MonoBehaviour
{
    public float TIME_TO_SHOW_DAMAGE_FROM_SPELLS = 0.3f;


    public Vector3 arrowOrigin;                 // Star position of arrow.
    public Vector3 arrowTarget;                 // End position of arrow.
    public LineRenderer cachedLineRenderer;     // Line renderer.
    public GameObject returnToHand;             // Game object that returns card to hand on hover.

    public bool isAttacking;                    // Prevents defending card from being selected for attacking used in Attack script.

    private PuzzleGameManager puzzleGameManager;

    void Start()
    {
        puzzleGameManager = GameObject.Find("GameManager").GetComponent<PuzzleGameManager>();

        isAttacking = false;

        cachedLineRenderer.enabled = false;
    }
    private void Update()
    {
        updateArrow();
        
        returnToHand.SetActive(isAttacking);
    }

    // Draws arrow.
    void updateArrow()
    {
        float adaptiveSize = 0.5f / Vector3.Distance(arrowOrigin, arrowTarget);
        cachedLineRenderer.SetPositions(new Vector3[] {
               arrowOrigin
               , Vector3.Lerp(arrowOrigin, arrowTarget, 0.999f - adaptiveSize)
               , Vector3.Lerp(arrowOrigin, arrowTarget, 1 - adaptiveSize)
               , arrowTarget });
    }

    /// <summary>
    /// Casts ray to get selected card to attack.
    /// </summary>
    /// <param name="attackingCard"></param>
    /// <returns></returns>
    public Transform getDefendingCard(Transform attackingCard)
    {
        // Raycasts all UI elements.
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);


        // Defending card.
        Transform defendingCard = null;

        // Gets defending card.
        foreach (RaycastResult result in results)
        {
            
            if (attackingCard == null && result.gameObject == returnToHand)
            {
                Transform transform = new GameObject().transform;
                transform.position = new Vector3(2000, 2000, 2000);
                return transform;
            }

            if ((result.gameObject.name.ToLower().Contains("background - ") && result.gameObject.GetComponentInParent<Canvas>().name.ToLower().Contains("board")) &&
                (attackingCard != result.gameObject.transform || attackingCard == null))
            {
                defendingCard = result.gameObject.transform;
                return defendingCard;
            }
        }

        return null;
    }

    /// <summary>
    /// Gets defending player
    /// </summary>
    /// <returns></returns>
    public Transform getDefendingPlayer()
    {
        // Raycasts all UI elements.
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Gets defending player.
        foreach (RaycastResult result in results)
        {
            string name = result.gameObject.transform.name.ToLower();
            if (name == "attackplayer")
            {
                return result.gameObject.transform;
            }
        }

        return null;
    }

    /// <summary>
    /// Moves card back to hand
    /// </summary>
    /// <param name="defendingCard"></param>
    public void moveCardBackToHand(GameObject spell)
    {
        foreach (MonoBehaviour script in spell.GetComponents<MonoBehaviour>())
        {
            if ( script.GetType().Name.ToLower().Contains("helper") ||
                 script.GetType().Name == "OnCardDestroy" ||
                 script.GetType().Name == "OnCardHoverInGame" )
                script.enabled = true;
            else
                script.enabled = false;
        }
            

        spell.transform.localPosition = Vector3.zero;
        puzzleGameManager.addCardToHand(spell);

        Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
        mana.useMana( -spell.GetComponent<CardCostHelper>().getCost() );

        isAttacking = false;
        cachedLineRenderer.enabled = false;
    }

    /// <summary>
    /// Gets all enemy cards
    /// </summary>
    /// <returns></returns>
    public List<Transform> getAllEnemyCards()
    {
        GameObject enemyBoard = GameObject.Find("EnemyBoard");
        List<Transform> cards = new List<Transform>();

        for (int i = 0; i < enemyBoard.transform.childCount; i++)
            cards.Add( enemyBoard.transform.GetChild(i).GetChild(0) );

        return cards;
    }

    public List<Transform> getAllPlayerCards()
    {
        GameObject playerBoard = GameObject.Find("PlayerBoard");
        List<Transform> cards = new List<Transform>();

        for (int i = 0; i < playerBoard.transform.childCount; i++)
            cards.Add( playerBoard.transform.GetChild(i).GetChild(0) );

        return cards;
    }

    public void SetPlayersCardsToAttack()
    {
        List<Transform> cards = getAllPlayerCards();
        foreach (Transform item in cards)
        {
            item.GetComponentInParent<Attack>().attacked = false;
        }
    }

    public void SetPlayersCardsToNotAttack()
    {
        List<Transform> cards = getAllPlayerCards();
        foreach (Transform item in cards)
        {
            Debug.Log(item.name);
            item.GetComponentInParent<Attack>().attacked = true;
        }
    }

}
