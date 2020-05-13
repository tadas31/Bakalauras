using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class Card : ScriptableObject
{
    public string cardName;             // Card's name.
    public int cost;                    // Card's cost.
    public Sprite image;                // Card's image.
    public string type;                 // Card's type.
    public int spellDamage;             // Card's spell damage (Effect damage).
    public int attack;                  // Card's attack.
    public int life;                    // Card's life.
    public List<string> scripts;        // Names list of scripts needed for card.

   

    /// <summary>
    /// Spawns the card.
    /// </summary>
    /// <returns>Returns the game object of the card.</returns>
    public GameObject spawnCard() 
    {
        //Getting the main card prefab.
        GameObject cardPrefab = Resources.Load<GameObject>("Cards/Prefabs/Card");
        //Getting the background of the card by type.
        Sprite background = getBackgroundSprite(type);
        //Creates the game object of the card.
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);

        newCard.name = cardName;

        System.Type scriptType;
        // Gets and adds necessary scripts to card.
        foreach (var script in scripts)
        {
            // Get's script type.
            scriptType = System.Type.GetType(script);

            // Adds script to card.
            if (scriptType != null)
            {
                (newCard.AddComponent(scriptType) as MonoBehaviour).enabled = false;
                var iSpellDamage = newCard.GetComponent(scriptType) as ISpellDamage;

                if (iSpellDamage != null)
                    iSpellDamage.setSpellDamage(spellDamage);
                else
                    continue;
                
            }
            else
            {
                continue;
            }
                
        }

        // If it's minion adds attack script.
        if (!type.ToLower().Contains("spell"))
        {
            // Add minion specific scripts
            newCard.AddComponent<Attack>().enabled = false;
            newCard.AddComponent<CardStatsHelper>().enabled = false;

            // Sets starting stats for minions
            newCard.GetComponent<CardStatsHelper>().startingAttack = attack;
            newCard.GetComponent<CardStatsHelper>().startingLife = life;


        }

        // Add scripts needed for all cards
        newCard.AddComponent<OnCardDestroy>();
        newCard.AddComponent<CardCostHelper>();
        newCard.AddComponent<CardDescriptionHelper>();
        newCard.AddComponent<OnCardHoverInGame>();

        // Set starting cost
        newCard.GetComponent<CardCostHelper>().startingCost = cost;

        //Sets all of the parameters of the card.
        newCard = setDataForCard(newCard, background);

        return newCard;
    }

    //spawns card with all data about it
    public GameObject spawnCard(GameObject cardPrefab, Sprite background, GameObject parent, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);    //creates new game object
        newCard.GetComponent<Transform>().SetParent(parent.transform);                      //sets parent
        newCard.transform.localPosition = new Vector3(x, y, 0);                             //sets cards position
        newCard.transform.localScale = Vector3.one;                                         //sets cards scale

        // Sets new anchor position for x.
        newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, newCard.GetComponent<RectTransform>().anchoredPosition.y);

        newCard.transform.name = cardName;

        System.Type scriptType;
        // Gets and adds necessary scripts to card.
        foreach (var script in scripts)
        {
            // Get's script type.
            scriptType = System.Type.GetType(script);

            // Adds script to card.
            if (scriptType != null)
            {
                (newCard.AddComponent(scriptType) as MonoBehaviour).enabled = false;
                var iSpellDamage = newCard.GetComponent(scriptType) as ISpellDamage;

                if (iSpellDamage != null)
                    iSpellDamage.setSpellDamage(spellDamage);
                else
                    continue;

            }
            else
            {
                continue;
            }

        }

        // Creates description
        newCard.AddComponent<CardDescriptionHelper>();

        //sets values to all card's fields
        newCard = setDataForCard(newCard, background);

        return newCard;
    }

    //spawns card only displaying cost name and amount of copies in deck
    public GameObject spawnCardCompact(GameObject cardPrefab, Sprite background, GameObject parent, int amountInDeck, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);    //creates new game object
        newCard.GetComponent<Transform>().SetParent(parent.transform);                      //sets parent
        newCard.transform.localPosition = new Vector3(x, y, 0);                             //sets cards position
        newCard.transform.localScale = Vector3.one;                                         //sets cards scale

        // Sets new anchor position for x.
        newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, newCard.GetComponent<RectTransform>().anchoredPosition.y);

        //sets values to fields
        newCard.transform.name = "CardInDeck - " + cardName;
        newCard.GetComponent<Image>().sprite = background;
        newCard.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = cost.ToString();
        newCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = cardName;
        newCard.transform.Find("AmountInDeck").GetComponent<TextMeshProUGUI>().text = "x" + amountInDeck.ToString();

        newCard.AddComponent<OnDeckCardHover>();

        return newCard;
    }

    /// <summary>
    /// Gets the background sprite by type.
    /// </summary>
    /// <param name="type">Type of the card.</param>
    /// <returns>Sprite of the background.</returns>
    private Sprite getBackgroundSprite(string type)
    {
        //If it is a spell card type returns spell background.
        if (type.ToLower().Contains("spell"))
        {
            return Resources.Load<Sprite>("Cards/Backgrounds/Spell");
        }
        //If there are no that meet the criteria returns the minion sprite.
        return Resources.Load<Sprite>("Cards/Backgrounds/Minion");
    }

    /// <summary>
    /// Sets all parameters for card's.
    /// </summary>
    /// <param name="card"> Card game object. </param>
    /// <param name="background"> Background for card. </param>
    /// <returns></returns>
    private GameObject setDataForCard(GameObject card, Sprite background)
    {
        //Sets all of the parameters of the card.
        foreach (var child in card.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "Background":
                    child.GetComponent<Image>().sprite = background;
                    child.transform.name = "Background - " + cardName;
                    break;
                case "Name":
                    child.GetComponent<TextMeshProUGUI>().text = cardName;
                    break;
                case "Cost":
                    child.GetComponent<TextMeshProUGUI>().text = cost.ToString();
                    break;
                case "Image":
                    child.GetComponent<Image>().sprite = image;
                    break;
                case "Type":
                    child.GetComponent<TextMeshProUGUI>().text = type;
                    break;
                case "Stats":
                    if (type.ToLower().Contains("spell"))
                        child.GetComponent<TextMeshProUGUI>().text = null;
                    else
                        child.GetComponent<TextMeshProUGUI>().text = attack + " / " + life;
                    break;
            }
        }
        return card;
    }
}
