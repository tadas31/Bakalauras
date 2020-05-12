using Xunit;

namespace Server
{
    public class UnitTest
    {

        /// Testing the CardContainer
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

        /// Testing Card code
        [Fact]
        public void SpawnCard()
        {
            Card card = new Card("goblin");
            Assert.Equal("goblin", card.cardName);
        }

        //Testing the player class
        [Fact]
        public void MakeNewPlayerCheckId()
        {
            Player player = new Player(4, "Username", "goblin");
            Assert.Equal(4, player.id);
        }

        [Fact]
        public void MakeNewPlayerCheckUsername()
        {
            Player player = new Player(4, "Username", "goblin");
            Assert.Equal("Username", player.username);
        }

        [Fact]
        public void MakeNewPlayerCheckDeck()
        {
            Player player = new Player(4, "Username", "goblin");
            Assert.Equal("goblin", player.deck.NamesToString());
        }


        [Fact]
        public void AddMaxManaPlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.AddMaxMana();
            Assert.Equal(2, player.maxMana);
        }


        [Fact]
        public void HasInHandTruePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            Assert.True(player.HasInHand("goblin"));
        }

        [Fact]
        public void HasInHandFalsePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            Assert.False(player.HasInHand("no"));
        }

        [Fact]
        public void HasEnoughManaFalsePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            Assert.False(player.HasEnoughMana("goblin"));
        }

        [Fact]
        public void HasEnoughManaTruePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.mana = 10;
            player.PullCardToHand();
            Assert.True(player.HasEnoughMana("goblin"));
        }

        [Fact]
        public void PullCardInHandPlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            Assert.Equal("goblin", player.hand.NamesToString());
        }


        [Fact]
        public void CardCountInHandPlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            Assert.Equal(1, player.CardCountInHand());
        }

        [Fact]
        public void PullStartingCardPlayer()
        {
            Player player = new Player(4, "Username", "goblin,goblin,goblin,goblin,goblin,goblin,goblin");
            player.PullStartingCards();
            Assert.Equal(Constants.START_CARD_COUNT, player.CardCountInHand());
        }

        [Fact]
        public void PutOnTablePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            player.PutOnTable("goblin");
            Assert.Equal(1,player.table.CardCount());
        }


        [Fact]
        public void DamagePlayersTableCardsPlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            player.PutOnTable("goblin");
            player.DamagePlayersTableCards(1);
            Assert.Equal(1, player.table.GetCard("goblin").life);
        }
        [Fact]
        public void ResetManaPlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.maxMana = Constants.MAX_MANA;
            player.ResetMana();
            Assert.Equal(Constants.MAX_MANA, player.mana);
        }
        [Fact]
        public void TableCardsCanAttackTruePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            player.PutOnTable("goblin");
            player.TableCardsCanAttack(true);
            Assert.True(player.table.GetCard("goblin").canAttack);
        }

        [Fact]
        public void TableCardsCanAttackFalsePlayer()
        {
            Player player = new Player(4, "Username", "goblin");
            player.PullCardToHand();
            player.PutOnTable("goblin");
            player.TableCardsCanAttack(false);
            Assert.False(player.table.GetCard("goblin").canAttack);
        }

        //
    }
}
