using Xunit;

namespace Server
{
    public class PlayerTests
    {
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
            Assert.Equal(1, player.table.CardCount());
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
    }
}
