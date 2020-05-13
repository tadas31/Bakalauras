using Xunit;

namespace Server
{
    public class CardContainerTests
    {
        [Fact]
        public void AddCardToContainerPassed()
        {
            CardContainer cards = new CardContainer("goblin");
            Assert.Equal(1, cards.CardCount());
        }

        [Fact]
        public void AddCardToContainerFailed()
        {
            CardContainer cards = new CardContainer("goblin");
            Assert.NotEqual(2, cards.CardCount());
        }

        [Fact]
        public void CheckIfTheCardIsInContainerTrue()
        {
            CardContainer cards = new CardContainer("goblin");
            Assert.True(cards.IsInDeck("goblin"));
        }

        [Fact]
        public void CheckIfTheCardIsInContainerFalse()
        {
            CardContainer cards = new CardContainer("goblin");
            Assert.False(cards.IsInDeck("FalseName"));
        }

        [Fact]
        public void PullCardFromCardContainer()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.PullCard();
            Assert.Equal("goblin", card.cardName);
        }

        [Fact]
        public void PullCardFromCardContainerWithName()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.PullCard("goblin");
            Assert.Equal("goblin", card.cardName);
        }
        [Fact]
        public void PullCardFromCardContainerCheckCount()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.PullCard();
            Assert.Equal(0, cards.CardCount());
        }
        [Fact]
        public void PullCardFromCardContainerWithNameCheckCount()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.PullCard("goblin");
            Assert.Equal(0, cards.CardCount());
        }

        [Fact]
        public void GetCardFormCardContainer()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.GetCard("goblin");
            Assert.Equal("goblin", card.cardName);
        }

        [Fact]
        public void GetCardFormCardContainerCardCount()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.GetCard("goblin");
            Assert.Equal(1, cards.CardCount());
        }

        [Fact]
        public void AddCardToDeck()
        {
            CardContainer cards = new CardContainer("goblin");
            Card card = cards.GetCard("goblin");
            cards.AddToDeck(card);
            Assert.Equal(2, cards.CardCount());
        }

        [Fact]
        public void DamageAllCardsCardContainerTest()
        {
            CardContainer cards = new CardContainer("goblin,raider");
            cards.DamageAllCards(1);
            Assert.Equal(1, cards.GetCard("goblin").life);
            Assert.Equal(2, cards.GetCard("raider").life);
        }

        [Fact]
        public void DamageAllCardsCardContainerOnDeathTest()
        {
            CardContainer cards = new CardContainer("goblin,rat");
            cards.DamageAllCards(3);
            Assert.Equal(0, cards.CardCount());
        }


        [Fact]
        public void NameToStringCardContainer()
        {
            CardContainer cards = new CardContainer("goblin,rat");
            Assert.Equal("goblin,rat", cards.NamesToString());
        }

        [Fact]
        public void SetCardsCanAttackTureCardContainer()
        {
            CardContainer cards = new CardContainer("goblin,rat");
            cards.SetCardsCanAttack(true);
            Assert.True(cards.GetCard("goblin").canAttack);
            Assert.True(cards.GetCard("rat").canAttack);
        }

        [Fact]
        public void SetCardsCanAttackFalseCardContainer()
        {
            CardContainer cards = new CardContainer("goblin,rat");
            cards.SetCardsCanAttack(false);
            Assert.False(cards.GetCard("goblin").canAttack);
            Assert.False(cards.GetCard("rat").canAttack);
        }
    }
}
