using System.Collections.Generic;
using ConquerTheDungeon.Logic.Effects;
using ConquerTheDungeon.Logic.Effects.PlayerEffects;

namespace ConquerTheDungeon.Logic.Perks;

public class PerksMap
{
    public class PerksLine
    {
        public readonly List<Perk> LeftPerks;

        public readonly List<Perk> RightPerks;

        public readonly Perk Base;

        public PerksLine(List<Perk> leftPerks, Perk @base, List<Perk> rightPerks)
        {
            LeftPerks = leftPerks;
            Base = @base;
            RightPerks = rightPerks;
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