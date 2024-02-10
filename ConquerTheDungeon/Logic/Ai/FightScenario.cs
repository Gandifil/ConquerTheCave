using System.Linq;
using ConquerTheDungeon.Logic.Cards;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace ConquerTheDungeon.Logic.Ai;

public class FightScenario
{
    private readonly FightScenarioContent _content;

    public FightScenario(FightScenarioContent content)
    {
        _content = content;
    }

    public CreatureCard[] GetCards(int turnIndex)
    {
        var cards = _content.Turns[turnIndex.ToString()];
        return cards.Select(x =>
                CardLoader.Get(x) as CreatureCard)
            .ToArray();
    }

    public static FightScenario LoadFromContent(string assetName) => 
        new FightScenario(Game1.Instance.Content.Load<FightScenarioContent>($"fights/{assetName}.json", new JsonContentLoader()));
}