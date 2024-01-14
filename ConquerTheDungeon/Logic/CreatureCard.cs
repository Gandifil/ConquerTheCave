using System.Collections.Generic;

namespace ConquerTheDungeon.Logic;

public class CreatureCard : Card
{
    public int Life => Content.Life;

    public int Damage => Content.Damage;

    public List<ModCard> Mods { get; set; }
    
    public CreatureCard(CardContent content) : base(content)
    {
    }
}