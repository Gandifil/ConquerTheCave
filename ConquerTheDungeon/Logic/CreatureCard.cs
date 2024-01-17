using System;
using System.Collections.Generic;
using ConquerTheDungeon.Animations;
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
        var animation = new StrikeCardAnimation(this, creature);
        animation.Finished += x => creature.Hurt(Damage);
        Game1.Instance.Animations.Add(animation);
    }

    public void Hurt(int value)
    {
        Life = Math.Max(0, Life - value);
        if (Life == 0)
            Die();
    }

    public event Action<CreatureCard> Died;

    private void Die()
    {
        Died?.Invoke(this);
    }
}