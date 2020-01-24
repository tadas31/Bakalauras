public interface ICardBuilder
{
    ICardBuilder setImage(string image);                //sets card's image
    ICardBuilder setType(string type);                  //sets card's type
    ICardBuilder setDescription(string description);    //sets card's description
    ICardBuilder setAttack(int attack);                 //sets card's attack
    ICardBuilder setLife(int life);                     //sets card's life

    AbstractCard Build();                               //returns card that was built
}
