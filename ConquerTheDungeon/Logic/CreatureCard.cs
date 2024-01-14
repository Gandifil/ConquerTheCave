namespace ConquerTheDungeon.Logic;

public class CreatureCard : Card
{
    public int Life => Content.Life;

    public int Damage => Content.Damage;
    
    public CreatureCard(CardContent content) : base(content)
    {
    }
}