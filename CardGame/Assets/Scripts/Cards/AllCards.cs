using System.Collections;

public class AllCards
{

    //hashtable of all cards in game 
    //key - card's title
    //value - all card's data
    //first value in CardBuilder is title second is cost
    private Hashtable cards = new Hashtable()
    {
        { "spellCard", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("CardImages/goblin_1").setType("SPELL").Build() },
        { "minionCard", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() }
    };

    public AllCards()
    {

    }

    //returns specific card
    public Card getCard(string title)
    {
        return (Card) cards[title];
    }

    //return all cards
    public Hashtable getCards()
    {
        return cards;
    }
}
