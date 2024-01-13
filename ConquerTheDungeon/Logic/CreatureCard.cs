namespace ConquerTheDungeon.Logic;

public class CreatureCard
{
    public readonly CardContent Content;

    public int Life => Content.Life;

    public int Damage => Content.Damage;
    
    public CreatureCard(CardContent content)
    {
        Content = content;
    }
}