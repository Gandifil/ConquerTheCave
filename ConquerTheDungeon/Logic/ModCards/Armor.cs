using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public class Armor: CounterModCard
{
    public Armor(CardContent content) : base(content)
    {
    }
    
    public Armor(CardContent content, int  value) : base(content, value)
    {
    }

    public override ModCard GetInitializedClone() => new Armor(Content, Content.BaseValue);
}