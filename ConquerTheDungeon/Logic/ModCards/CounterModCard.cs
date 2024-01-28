using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public abstract class CounterModCard: ModCard
{
    public int Value { get; protected set; }
    
    public CounterModCard(CardContent content) : base(content)
    {
    }
    
    public CounterModCard(CardContent content, int value) : base(content)
    {
        Value = value;
    }


    public override bool CanBeAddedTo(CreatureCard creature) => true;

    public override void Append(ModCard card)
    {
        if (card is CounterModCard counter)
            Value += counter.Content.BaseValue;
    }
}