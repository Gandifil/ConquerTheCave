namespace ConquerTheDungeon.Logic.Cards.Spells;

public abstract class SpellCard: Card
{
    public TargetKind TargetKind => Content.TargetKind;
    
    public TargetSide TargetSide => Content.TargetSide;
    
    public SpellCard(CardContent content) : base(content)
    {
    }

    public abstract void Use(CreatureCard target);
}