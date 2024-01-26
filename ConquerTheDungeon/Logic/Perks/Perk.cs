using ConquerTheDungeon.Logic.Effects;

namespace ConquerTheDungeon.Logic.Perks;

public class Perk
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public EffectsList<Player> Effects { get; set; }
    
    public string ImageName { get; set; }
}