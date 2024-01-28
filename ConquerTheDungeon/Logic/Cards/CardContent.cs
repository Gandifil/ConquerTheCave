using System.Text.Json.Serialization;
using ConquerTheDungeon.Logic.Cards.Spells;

namespace ConquerTheDungeon.Logic.Cards;

public class CardContent
{
    // it's for logic
    [JsonIgnore]
    public string Id { get; set; }

    public string Name { get; set; }
    
    public string Type { get; set; }
    
    public string Texture { get; set; }

    public int Damage { get; set; }

    public int  Life { get; set; }
    
    public TargetKind TargetKind { get; set; }
    
    public TargetSide TargetSide { get; set; }
    
    public int BaseValue { get; set; }
}