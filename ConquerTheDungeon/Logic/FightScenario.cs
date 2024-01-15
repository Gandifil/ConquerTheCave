using System.Linq;

namespace ConquerTheDungeon.Logic;

public class FightScenario
{
    public CreatureCard[] GetInitialCards()
    {
        var cards = new string[] { "dog01", "dog01"};
        return cards.Select(x =>
                CardLoader.Get(x) as CreatureCard)
            .ToArray();
    }
}