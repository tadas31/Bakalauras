using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassicGameManager : MonoBehaviour
{
    public static ClassicGameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static PlayerManager curPlayer;

    public GameObject localPlayer;
    public GameObject enemyPlayer;
    public GameObject handCanvas;
    //TODO: Maybe change that the localPLayer and enemy prefabs would be spawned as board elements.
    public GameObject playerBoard;
    public GameObject enemyBoard;
    public AttackHelper attackHelper;
    public TextMeshProUGUI deckCount;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = localPlayer;
            curPlayer = _player.GetComponent<PlayerManager>() ;
        }
        else
        {
            _player = enemyPlayer;
            UIManager.instance.ActivateEndTurn();
            UIManager.instance.ActivateTimer();
        }

        _player.SetActive(true);
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;

        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void PullStartingCards(string _deck)
    {
        string[] values = _deck.Split(',');
        foreach (string value in values)
        {
            AddCardToHand(value);
        }
    }

    public void SetTurn(bool _isTurn)
    {
        curPlayer.isTurn = _isTurn;
        //Changes if the card can attack or cannot.
        if (_isTurn)
        {
            playerBoard.GetComponentInParent<AttackHelper>().SetPlayersCardsToAttack();
        }
        else 
        {
            playerBoard.GetComponentInParent<AttackHelper>().SetPlayersCardsToNotAttack();
        }
    }

    public void SetLife(int _clientId, int _amount)
    {
        foreach (PlayerManager item in players.Values)
        {
            if (_clientId == item.id)
            {
                item.life = _amount;
            }
        }
    }

    public void SetMana(int _clientId, int _amount)
    {
        foreach (PlayerManager item in players.Values)
        {
            if (_clientId == item.id)
            {
                item.mana = _amount;
            }
        }
    }

    public void SetMaxMana(int _clientId, int _amount)
    {
        foreach (PlayerManager item in players.Values)
        {
            if (_clientId == item.id)
            {
                item.maxMana = _amount;
            }
        }
    }

    public void SetDeckCardCount(int _count)
    {
        deckCount.text = _count.ToString();
    }

    public void Attack(int _clientFrom, string _from, int _fromLife, string _to, int _toLife)
    {
        Transform attackingCard;
        Transform defendingCard;
        Debug.Log(_from);

        if (Client.instance.myId == _clientFrom)
        {
            attackingCard = playerBoard.transform.Find(_from);
            defendingCard = enemyBoard.transform.Find(_to);
        }
        else
        {
            attackingCard = enemyBoard.transform.Find(_from);
            defendingCard = playerBoard.transform.Find(_to);
        }
        if (_from == "Fire ball")
        {
            StartCoroutine(castSpell(defendingCard.GetChild(0), 1, "card"));
        }
        else
        if (_from == "Lightning strike")
        {
            StartCoroutine(castSpell(defendingCard.GetChild(0), 5, "card"));
        }
        else
        if (_from == "Meteor")
        {
            Destroy(defendingCard.gameObject);
        }
        else
            attackingCard.GetComponent<Attack>().AttackAnimationToCard(defendingCard);
    }

    /// <summary>
    /// Gets card that was pressed and attacks it
    /// </summary>
    private IEnumerator castSpell(Transform defending,int damage, string defenderType)
    {
        attackHelper.cachedLineRenderer.enabled = false;

        if (defenderType == "card")
        {
            CardStatsHelper defendingCardStats = defending.GetComponentInParent<CardStatsHelper>();

            // Deals damage to defending card
            defendingCardStats.takeDamage(damage);

            // Displays damage dealt to defending card
            TextMeshProUGUI defendingCardDamageTaken = defending.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
            defendingCardDamageTaken.text = "-" + damage;
            yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
            defendingCardDamageTaken.text = null;

            // Destroys card if its dead
            defendingCardStats.checkIfSitllAlive();
        }
        else
        {
            Health playerHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
            playerHealth.takeDamage(damage);
        }

        // Resets all booleans to allow attack with another card
        attackHelper.isAttacking = false;
        Destroy(GameObject.Find("Spells").transform.GetChild(0).gameObject);
    }


    /// <summary>
    /// Attacks player with some of the cards.
    /// </summary>
    /// <param name="_clientFrom">The client that is attacking.</param>
    /// <param name="_clientTo">The client that is attacked.</param>
    /// <param name="_from">With what card is attacked</param>
    /// <param name="_toLife">The life that is of enemy players</param>
    public void AttackPlayer(int _clientFrom, int _clientTo, string _from, int _toLife) 
    {
        Transform attackingCard;
        Health playerHealth;

        if (_from == "Fire ball" || _from == "Lightning strike")
        {
            if (Client.instance.myId == _clientFrom)
            {
                playerHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
            }
            else
            {
                playerHealth = GameObject.Find("Canvas/Player").GetComponent<Health>();
            }

            if (_from == "Fire ball")
            {
                playerHealth.takeDamage(1);
            }
            else
            if (_from == "Lightning strike")
            {
                playerHealth.takeDamage(5);
            }

            // Resets all booleans to allow attack with another card
            attackHelper.isAttacking = false;
            Destroy(GameObject.Find("Spells").transform.GetChild(0).gameObject);

            if (_toLife <= 0)
            {
                playerHealth.gameObject.GetComponent<PlayerManager>().loseWinConditions.SetActive(true);
                PlayerManager.hasTurnOn = true;
            }
            return;
        }

        if (Client.instance.myId == _clientFrom)
        {
            attackingCard = playerBoard.transform.GetChild(BoardCards.attackingCard);
            playerHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
        }
        else
        {
            attackingCard = enemyBoard.transform.Find(_from);
            playerHealth = GameObject.Find("Canvas/Player").GetComponent<Health>();
        }
            
        attackingCard.GetComponent<Attack>().AttackAnimationToPlayer(playerHealth);

        if (_toLife <= 0)
        {
            playerHealth.gameObject.GetComponent<PlayerManager>().loseWinConditions.SetActive(true);
            PlayerManager.hasTurnOn = true;
        }
    }

    public void PutOnTable(string _cardName, bool _isPlayers, bool removeFromHand = true)
    {

        Card card = Resources.Load<Card>("Cards/CreatedCards/" + _cardName);
        GameObject cardTable = card.spawnCard();
        cardTable.name = ChangeName(_cardName, _isPlayers);
        if (_isPlayers)
        {
            if (cardTable.transform.GetChild(0).Find("Type").GetComponent<TextMeshProUGUI>().text.ToLower().Contains("spell"))
            {
                //Changes the parent of the card to spells.
                GameObject spells = GameObject.Find("Spells");
                cardTable.transform.SetParent(spells.transform);
                cardTable.transform.localScale = Vector3.one;
            }
            else
            {
                cardTable.transform.SetParent(playerBoard.transform);
                cardTable.transform.localScale = Vector3.one;
                cardTable.GetComponent<Attack>().enabled = true;
                cardTable.GetComponent<CardStatsHelper>().enabled = true;
            }

            if (removeFromHand)
                RemoveCardFromHand(_cardName);
           
            //Enables all attached scripts.
            foreach (MonoBehaviour script in cardTable.GetComponents<MonoBehaviour>())
                script.enabled = true;

            if (_cardName == "Circle of mana")
            {
                Destroy(cardTable);
            }
        }
        else
        {
            if (cardTable.transform.GetChild(0).Find("Type").GetComponent<TextMeshProUGUI>().text.ToLower().Contains("spell"))
            {
                //Changes the parent of the card to spells.
                GameObject spells = GameObject.Find("SpellsEnemy");
                cardTable.transform.SetParent(spells.transform);
                cardTable.transform.localScale = Vector3.one;
                //Enables all attached scripts.
                foreach (MonoBehaviour script in cardTable.GetComponents<MonoBehaviour>())
                    script.enabled = true;
            }
            else
            {
                cardTable.transform.SetParent(enemyBoard.transform);
                cardTable.GetComponent<Attack>().enabled = true;
                cardTable.GetComponent<CardStatsHelper>().enabled = true;
                cardTable.transform.localScale = Vector3.one;
            }
        }
    }

   /// <summary>
   /// Changes the name of the card if there is a same card there
   /// </summary>
   /// <param name="_isPlayers"></param>
    public string ChangeName(string _cardName, bool _isPlayers)
    {
        int numb = -1;
        List<Transform> cardList = new List<Transform>();

        //If it is players return the players card if not then return enemy cards
        if (_isPlayers)
        {
            cardList = attackHelper.getAllPlayerCards();
        }
        else
        {
            cardList = attackHelper.getAllEnemyCards();
        }
        //Go through the list
        foreach (Transform item in cardList)
        {
            string name = item.parent.name;
            
            //If the name is the same make calculations
            if (name.Contains(_cardName))
            {
                if (name == _cardName)
                {
                    numb = 1;
                }
                else
                {
                    char c = name[name.Length - 1];
                    int a;
                    //If there is a number at the back then add it.
                    if (int.TryParse(new string(c, 1), out a))
                    {
                        if (numb <= a)
                        {
                            numb = a + 1;
                        }
                    }
                }
            }
        }

        if (numb > 0)
        {
            return _cardName + numb;
        }

        return _cardName;
    }

    public void AddCardToHand(string cardName)
    {
        Card card = Resources.Load<Card>("Cards/CreatedCards/" + cardName);
        GameObject cardHand = card.spawnCard();
        //GameObject addedCard = Instantiate(card,new Vector3(handCards.transform.position.x, handCards.transform.position.y, handCards.transform.position.z), Quaternion.identity);
        cardHand.transform.localScale = handCanvas.transform.localScale;
        cardHand.AddComponent<CardInHand>();
        cardHand.transform.SetParent(handCanvas.transform);
        HandReorganize();
    }

    //Adds card to hand without the servers help, use if it is as a spell
    public void AddCardToHand(GameObject addedCard)
    {
        addedCard.transform.localScale = handCanvas.transform.localScale;
        addedCard.AddComponent<CardInHand>();
        addedCard.transform.SetParent(handCanvas.transform);
        HandReorganize();
    }

    public void RemoveCardFromHand(string cardName)
    {
        int cardNumber = CardInHand.placedCard;
        //If there is a saved card that was put on the table and it's name is the same then put it
        if (CardInHand.placedCard >= 0 && handCanvas.transform.childCount - 1 >= cardNumber && handCanvas.transform.GetChild(cardNumber).name == cardName)
        {
            Transform card = handCanvas.transform.GetChild(cardNumber);
            Destroy(card.gameObject);
            CardInHand.placedCard = -1;
        }
        else
        {
            foreach (Transform child in handCanvas.transform)
            {
                if (child.name == cardName)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
        }
        HandReorganize();
    }

    /// <summary>
    /// Reorganizes the had by the cards in hand
    /// </summary>
    public void HandReorganize()
    {
        Debug.Log("Reorganizing hand");
        //Spacing between cards
        float spacing = 100f;
        //The count of cards in the hand
        int count = handCanvas.transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in handCanvas.transform)
        {
            item.localPosition = new Vector3(positionX, item.localPosition.y);
            positionX += spacing;
        }
    }

    private void OnDestroy()
    {
        players = new Dictionary<int, PlayerManager>();
    }
}
