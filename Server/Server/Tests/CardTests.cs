
using Xunit;

namespace Server
{
    public class CardTests
    {
        [Fact]
        public void SpawnCard()
        {
            Card card = new Card("goblin");
            Assert.Equal("goblin", card.cardName);
        }
    }
}
