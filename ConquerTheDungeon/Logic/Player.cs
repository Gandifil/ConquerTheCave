using System.Linq;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace ConquerTheDungeon.Logic;

public class Player
{
    public CreatureCard[] Cards { get; private set; }

    public Player()
    {
        Cards = LoadStartCards();
    }

    private CreatureCard[] LoadStartCards()
    {
        var cards = new string[] { "warrior", "mage" };
        return cards.Select(x =>
            new CreatureCard(Game1.Instance.Content.Load<CardContent>($"cards/{x}.json", new JsonContentLoader())))
            .ToArray();
    }
}