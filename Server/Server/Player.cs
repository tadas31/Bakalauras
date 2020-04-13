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

        public Deck deck;
        public Deck hand;
        public Deck graveYard;
        public Deck table;

        public Player(int _id, string _username, string _dataDeck)
        {
            id = _id;
            username = _username;
            isTurn = false;
            life = Constants.START_LIFE;
            mana = 1;
            deck = new Deck(_dataDeck);
            hand = new Deck();
            graveYard = new Deck();
            table = new Deck();
        }

        public string PullStartingCards()
        {
            for (int i = 0; i < Constants.START_CARD_COUNT; i++)
            {
                PullCardToHand();
            }
            return hand.NamesToString();
        }

        public void PullCardToHand()
        {
            hand.AddToDeck(deck.PullCard());
        }

        public bool HasInHand(string _cardName)
        {
            return hand.isInDeck(_cardName);
        }

        public void PutToTable(string _cardName)
        {
            table.AddToDeck(hand.PullCard(_cardName));
        }

        public void AddMana()
        {
            if (mana < Constants.MAX_MANA)
            {
                mana++;
            }
        }
    }
}
