using System.Linq;

namespace ConquerTheDungeon.Logic;

public class Player
{
    public Card[] Cards { get; private set; }

    public Player()
    {
        Cards = LoadStartCards();
    }

    private Card[] LoadStartCards()
    {
        var cards = new string[] { "warrior", "mage", "armor" };
        return cards.Select(x =>
            CardLoader.Get(x))
            .ToArray();
    }
}