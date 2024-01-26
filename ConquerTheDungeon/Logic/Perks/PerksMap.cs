using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic.Effects;
using ConquerTheDungeon.Logic.Effects.PlayerEffects;

namespace ConquerTheDungeon.Logic.Perks;

public class PerksMap
{
    public class PerkHandler
    {
        public readonly Perk Perk;

        public PerkHandler(Perk perk)
        {
            Perk = perk;
        }

        public bool IsEnabled { get; private set; }

        public event Action<Perk> Enabled;

        public void Enable()
        {
            Enabled?.Invoke(Perk);
            IsEnabled = true;
        }
    }
    
    public class PerksLine
    {
        public readonly List<PerkHandler> LeftPerks;

        public readonly List<PerkHandler> RightPerks;

        public readonly PerkHandler Base;

        public PerksLine(List<Perk> leftPerks, Perk @base, List<Perk> rightPerks)
        {
            LeftPerks = leftPerks.Select(x => new PerkHandler(x)).ToList();
            Base = new PerkHandler(@base);
            RightPerks = rightPerks.Select(x => new PerkHandler(x)).ToList();
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
            new PerksLine(new List<Perk>{perk, perk}, perk, new List<Perk>())
        };
    }
}