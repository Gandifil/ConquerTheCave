using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public class EnergyShield: CounterModCard
{
    public EnergyShield(CardContent content) : base(content)
    {
    }
    
    public EnergyShield(CardContent content, int value) : base(content, value)
    {
    }
    
    public override ModCard Clone() => new EnergyShield(Content, Content.BaseValue);
}