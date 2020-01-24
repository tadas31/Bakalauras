//public class CardBuilder : ICardBuilder
//{
//    private Basic tempCard;      //card that is being built

//    public CardBuilder(string title, int cost, string image, string type)
//    {
//        tempCard = new Basic(title, cost, image, type);

//    }

//    //returns card that was built
//    public AbstractCard Build()
//    {
//        return tempCard;
//    }

//    //sets card's attack
//    public ICardBuilder setAttack(int attack)
//    {
//        tempCard.attack = attack;
//        return this;
//    }

//    //sets card's description
//    public ICardBuilder setDescription(string description)
//    {
//        tempCard.description = description;
//        return this;
//    }

//    //sets card's image
//    public ICardBuilder setImage(string image)
//    {
//        tempCard.image = image;
//        return this;
//    }

//    //sets card's life
//    public ICardBuilder setLife(int life)
//    {
//        tempCard.life = life;
//        return this;
//    }

//    //sets card's type
//    public ICardBuilder setType(string type)
//    {
//        tempCard.type = type;
//        return this;
//    }
//}
