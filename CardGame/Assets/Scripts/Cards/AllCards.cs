//using System.Collections;

//public class AllCards
//{

//    //hashtable  of all cards in game 
//    //key - card's title
//    //value - all card's data
//    //first value in CardBuilder is title second is cost
//    private Hashtable cards = new Hashtable()
//    {
//        { "spellCard", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },


//        //random cards for testing
//        { "spellCard1", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard2", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "spellCard3", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard4", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "spellCard5", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard6", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "spellCard7", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard8", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "spellCard9", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "minionCard0", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "1minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "2minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "3minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "4minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "5minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "6minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "7minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "8minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "9minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "0minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "aminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "qwminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "wminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "eminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "rminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "tminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "yminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "uminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "iminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "ominionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "pminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "[minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "]minionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "sminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "dminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "fminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "gminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "hminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "jminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { "kminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "lminionCard01", new CardBuilder("spellCard", 5).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("SPELL").Build() },
//        { ";minionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },
//        { "zminionCard01", new CardBuilder("minionCard", 2).setDescription("TEXT").setImage("Cards/CardImages/goblin_1").setType("GOBLIN").setAttack(1).setLife(5).Build() },

//    };

//    public AllCards()
//    {

//    }

//    //returns specific card
//    public Card getCard(string title)
//    {
//        return (Card) cards[title];
//    }

//    //return all cards
//    public Hashtable getCards()
//    {
//        return cards;
//    }
//}
