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

        public Card PullCard()
        {
            int i = rand.Next(0, deck.Count);
            Card value = deck[i];
            deck.RemoveAt(i);
            return value;
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
    }
}
