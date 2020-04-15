using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Deck
    {
        private static Random rand = new Random();
        private List<Card> deck;

        public Deck()
        {
            deck = new List<Card>();
        }

        public Deck(string data)
        {
            deck = new List<Card>();
            string[] values = data.Split(',');
            foreach(string value in values)
            {
                deck.Add(new Card(value));
            }
        }

        /// <summary>
        /// Gets the card from the array.
        /// </summary>
        /// <param name="_cardName">The name of the card</param>
        /// <returns>The reference to the cards</returns>
        public Card GetCard(string _cardName)
        {
            foreach (Card item in deck)
            {
                if (_cardName == item.cardName)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Pulls and removes the card from the card array.
        /// </summary>
        /// <returns>The card.</returns>
        public Card PullCard()
        {
            int i = rand.Next(0, deck.Count);
            Card value = deck[i];
            deck.RemoveAt(i);
            return value;
        }

        /// <summary>
        /// Gets the card form the made card list and removes it.
        /// </summary>
        /// <param name="_cardName">The name of the pulled card.</param>
        /// <returns>The card that was pulled from the list array.</returns>
        public Card PullCard(string _cardName)
        {
            foreach (Card card in deck)
            {
                if (card.cardName == _cardName)
                {
                    deck.Remove(card);
                    return card;
                }
            }
            return null;
        }

        public void AddToDeck(Card card)
        {
            deck.Add(card);
        }

        public string NamesToString()
        {
            string tmp = null;
            foreach (Card card in deck)
            {
                if (string.IsNullOrEmpty(tmp))
                {
                    tmp = card.cardName;
                }
                else
                {
                    tmp += "," + card.cardName;
                }
            }
            return tmp;
        }

        

        public bool IsInDeck(string _cardName) 
        {
            foreach (Card _card in deck)
            {
                if (_card.cardName == _cardName)
                {
                    return true;
                }
            }
            return false;
        }

        public int CardCount()
        {
            return deck.Count;
        }
    }
}
