using System;
using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public abstract class CounterModCard: ModCard
{
    private int _value;

    public int Value
    {
        get => _value;
        protected set
        {
            _value = Math.Max(0, value);
            if (_value == 0)
                Cancel();
        }
    }
    
    public CounterModCard(CardContent content) : base(content)
    {
    }
    
    public CounterModCard(CardContent content, int value) : base(content)
    {
        Value = value;
    }

    public override int PreDamage(int value)
    {
        var blockedDamage = Math.Min(Value, value);
        Value -= blockedDamage;
        return value - blockedDamage;
    }

    public override bool CanBeAddedTo(CreatureCard creature) => true;

    public override void Append(ModCard card)
    {
        if (card is CounterModCard counter)
            Value += counter.Content.BaseValue;
    }
}