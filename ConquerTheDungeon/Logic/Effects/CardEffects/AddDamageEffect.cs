using System;
using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.Effects.CardEffects;

public class AddDamageEffect: Effect<Card>
{
    public override string Description => "+1 к урону";

    public override void Commit(Card item)
    {
        if (item is CreatureCard card)
        {
            card.Damage++;
        }
        else throw new ApplicationException("Card must be creature!");
    }

    public override void Rollback(Card item)
    {
        if (item is CreatureCard card)
        {
            card.Damage--;
        }
        else throw new ApplicationException("Card must be creature!");
    }
}