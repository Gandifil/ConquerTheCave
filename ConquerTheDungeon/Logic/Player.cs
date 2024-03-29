using System;
using System.Linq;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.Perks;

namespace ConquerTheDungeon.Logic;

public class Player
{
    public Card[] Cards { get; private set; }

    public readonly PerksMap PerksMap = new PerksMap();

    public Player()
    {
        Cards = LoadStartCards();
    }

    public int PerkPoints { get; set; } = 1;

    private Card[] LoadStartCards()
    {
        var cards = new [] { "turret01", "energy_shield", "handgunShoot"};
        return cards.Select(x =>
            CardLoader.Get(x))
            .ToArray();
    }

    public void TeachPerk(PerkHandler handler)
    {
        if (PerkPoints > 0 && !handler.IsEnabled && handler.CanEnable)
        {
            handler.Enable();
            handler.Perk.Effects.Commit(this);
            PerksMap.ResetCanEnabled();
            PerkPoints--;
        }
    }
}