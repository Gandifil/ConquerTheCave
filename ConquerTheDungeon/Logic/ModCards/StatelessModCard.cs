using System;
using System.Linq;
using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public abstract class StatelessModCard: ModCard
{
    public StatelessModCard(CardContent content) : base(content)
    {
    }

    public override bool CanBeAddedTo(CreatureCard creature) => creature.Mods.All(x => x.GetType() != GetType());

    public override void Append(ModCard card) => throw new ApplicationException("StatelessModCard cannot append");
}