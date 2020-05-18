using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Player
    {
        public int id;
        public string username;
        public bool isTurn;
        public int life;
        public int mana;
        public int maxMana;

        public CardContainer deck;
        public CardContainer hand;
        public CardContainer graveYard;
        public CardContainer table;

        public Player(int _id, string _username, string _dataDeck)
        {
            id = _id;
            username = _username;
            isTurn = false;
            life = Constants.START_LIFE;
            mana = 1;
            maxMana = 1;
            deck = new CardContainer(_dataDeck);
            hand = new CardContainer();
            graveYard = new CardContainer();
            table = new CardContainer();
        }

        public string PullStartingCards()
        {
            for (int i = 0; i < Constants.START_CARD_COUNT; i++)
            {
                PullCardToHand();
            }
            return hand.NamesToString();
        }

        public Card PullCardToHand()
        {
            if (deck.CardCount() <= 0) 
            {
                return null;
            }

            Card _card = deck.PullCard();
            hand.AddToDeck(_card);
            return _card;
        }

        public bool HasInHand(string _cardName)
        {
            return hand.IsInDeck(_cardName);
        }

        public bool HasEnoughMana(string _cardName)
        {
            Card _card = hand.GetCard(_cardName);
            if (_card.cost <= mana)
            {
                return true;
            }
            return false;
        }

        public void PutOnTable(string _cardName, bool _fromHand = true)
        {
            Card _card;
            if (_fromHand)
            {
                _card = hand.PullCard(_cardName);
            }
            else
            {
                _card = table.GetCard(_cardName);
            }

            //Check if the card has charge
            foreach (string item in _card.scripts)
            {
                if (item.ToLower().Contains("charge"))
                {
                    _card.canAttack = true;
                }
            }
            mana -= _card.cost;
            if (!_card.type.ToLower().Contains("spell"))
            {
                table.AddToDeck(_card);
            }
        }

        public void TableCardsCanAttack(bool _canAttack)
        {
            table.SetCardsCanAttack(_canAttack);
        }
        public void AddMaxMana()
        {
            if (maxMana < Constants.MAX_MANA)
            {
                maxMana++;
            }
        }

        public void ResetMana()
        {
            mana = maxMana;
        }

        public int CardCountInHand()
        {
            return hand.CardCount();
        }

        public void DamagePlayersTableCards(int damage) 
        {
            table.DamageAllCards(damage);
        }
    }
}
