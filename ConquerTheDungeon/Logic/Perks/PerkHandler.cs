using System;

namespace ConquerTheDungeon.Logic.Perks;

public class PerkHandler
{
    public readonly Perk Perk;
    
    public readonly int LineIndex;
    
    public readonly int PlaceIndex;

    public PerkHandler(Perk perk, int lineIndex, int placeIndex)
    {
        Perk = perk;
        LineIndex = lineIndex;
        PlaceIndex = placeIndex;
    }

    public bool IsEnabled { get; private set; }

    public bool CanEnable { get; set; }

    public event Action<Perk> Enabled;

    public void Enable()
    {
        Enabled?.Invoke(Perk);
        IsEnabled = true;
    }
}