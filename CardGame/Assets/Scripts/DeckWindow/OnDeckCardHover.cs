using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDeckCardHover : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler
{
    private float POS_X = 350f;         // Card x position.
    private float POS_Y = 248f;         // Card y position.

    private GameObject cardPrefab;      // Card prefab.
    private GameObject parent;          // Card parent.
    private Sprite minionBackground;    // Minion card background.
    private Sprite spellBackground;     // Spell card background.

    private GameObject createdCard;     // Created card.

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = Resources.Load<GameObject>("Cards/Prefabs/Card");
        minionBackground = Resources.Load<Sprite>("Cards/Backgrounds/Minion");
        spellBackground = Resources.Load<Sprite>("Cards/Backgrounds/Spell");
        parent = GameObject.Find("DisplayHoveredCard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Displays card that is hovered over
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        List<string> split = transform.name.Split(' ').ToList();
        split.RemoveRange(0, 2);
        string cardName = String.Join(" ", split);

        Card c = Resources.Load<Card>("Cards/CreatedCards/" + cardName);

        //picks background according to card type
        if (c.type.ToLower().Contains("spell"))
            createdCard = c.spawnCard(cardPrefab, spellBackground, parent, POS_X, POS_Y);     //spawns spell
        else
            createdCard = c.spawnCard(cardPrefab, minionBackground, parent, POS_X, POS_Y);    //spawns minion
    }

    /// <summary>
    /// Deletes card that was hovered over
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {

        //destroys all children of content object
        foreach (Transform child in parent.transform)
            Destroy(child.gameObject);
    }
}