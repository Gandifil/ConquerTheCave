using System;
using System.Collections.Generic;
using MLEM.Extensions;
using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic;

public class CreatureCard : Card
{
    private readonly List<ModCard> _mods = new();
    public int Life {get; private set; }

    public int Damage => Content.Damage;

    public event EventHandler<ModCard> OnModAdded;

    public IReadOnlyList<ModCard> Mods => _mods;

    public CreatureCard(CardContent content) : base(content)
    {
        Life = content.Life;
    }

    public void Add(ModCard mod)
    {
        _mods.Add(mod);
        OnModAdded?.Invoke(this, mod);
    }

    public CreatureCard Clone()
    {
        return new CreatureCard(Content);
    }

    public void Turn(ObservableCollection<CreatureCard> ownBoard, ObservableCollection<CreatureCard> againstBoard)
    {
        var creature = Random.Shared.GetRandomEntry(againstBoard);
        creature.Hurt(Damage);
    }

    public void Hurt(int value)
    {
        Life -= value;
    }
}