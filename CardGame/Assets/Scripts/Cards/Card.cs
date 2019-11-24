public class Card
{
    public string title { get; set; }               //card's title
    public int cost { get; set; }                   //card's cost
    public string image { get; set; }               //card's image
    public string type { get; set; }                //card's type
    public string description { get; set; }         //card's description
    public int attack { get; set; }                 //card's attack
    public int life { get; set; }                   //card's life
    
   public Card(string title, int cost)
    {
        this.title = title;
        this.cost = cost;
    }
}
