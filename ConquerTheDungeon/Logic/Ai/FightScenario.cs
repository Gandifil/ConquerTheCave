using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic.Cards;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace ConquerTheDungeon.Logic.Ai;

public class FightScenario
{
    private readonly FightScenarioContent _content;
    private readonly Queue<(int turn, string[] cards)> _queue = new();

    public FightScenario(FightScenarioContent content)
    {
        _content = content;
        foreach (var pair in _content.Turns)
            _queue.Enqueue((int.Parse(pair.Key), pair.Value));
    }

    public CreatureCard[] GetCards(int turnIndex, Board playerBoard)
    {
        return _queue.TryPeek(out var first) && (first.turn == turnIndex || !playerBoard.Creatures.Any())
            ? Spawn()
            : Array.Empty<CreatureCard>();
    }

    private CreatureCard[] Spawn()
    {
        var action = _queue.Dequeue();
        return action.cards.Select(x => CardLoader.Get(x) as CreatureCard).ToArray();
    }

    public static FightScenario LoadFromContent(string assetName) => 
        new (Game1.Instance.Content.Load<FightScenarioContent>($"fights/{assetName}.json", new JsonContentLoader()));
}