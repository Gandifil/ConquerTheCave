using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic.Effects;
using ConquerTheDungeon.Logic.Effects.PlayerEffects;

namespace ConquerTheDungeon.Logic.Perks;

public class PerksMap
{
    public class PerksLine
    {
        public readonly List<PerkHandler> LeftPerks;

        public readonly List<PerkHandler> RightPerks;

        public readonly PerkHandler Base;

        public PerksLine(int index, List<Perk> leftPerks, Perk @base, List<Perk> rightPerks)
        {
            Base = new PerkHandler(@base, index, 0);
            
            LeftPerks = leftPerks.Select((x, i) => new PerkHandler(x, index, -i)).ToList();
            RightPerks = rightPerks.Select((x, i) => new PerkHandler(x, index, i)).ToList();
            Base.Enable();
        }
    }

    public readonly PerksLine[] Lines;

    public PerksMap()
    {
        var perk = new Perk()
        {
            Name = "",
            Description = "",
            ImageName = "double_strike",
            Effects = new EffectsList<Player>{CardEffect.GetAddDamageToCard("mage")},
        };
        Lines = new PerksLine[]
        {
            new PerksLine(0,new List<Perk>{perk, perk}, perk, new List<Perk>())
        };
        ResetCanEnabled();
    }

    public void ResetCanEnabled()
    {
        foreach (var line in Lines)
        {
            ResetCanEnabled(line.LeftPerks);
            ResetCanEnabled(line.RightPerks);
        }
    }

    private void ResetCanEnabled(List<PerkHandler> perks)
    {
        for (int i = 0; i < perks.Count; i++)
        {
            perks[i].CanEnable.Value = (i > 0 ? perks[i - 1].IsEnabled : true) && !perks[i].IsEnabled;
        }
    }
}