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
            LeftPerks = leftPerks.Select((x, i) => new PerkHandler(x, index, -i)).ToList();
            Base = new PerkHandler(@base, index, 0);
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
    }
}