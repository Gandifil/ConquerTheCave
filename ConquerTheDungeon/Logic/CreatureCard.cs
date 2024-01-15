using System;
using System.Collections.Generic;

namespace ConquerTheDungeon.Logic;

public class CreatureCard : Card
{
    private readonly List<ModCard> _mods = new();
    public int Life => Content.Life;

    public int Damage => Content.Damage;

    public event EventHandler<ModCard> OnModAdded;

    public IReadOnlyList<ModCard> Mods => _mods;
    
    public CreatureCard(CardContent content) : base(content)
    {
    }

    public void Add(ModCard mod)
    {
        _mods.Add(mod);
        OnModAdded?.Invoke(this, mod);
    }
}