using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerTheDungeon.Animations;
using ConquerTheDungeon.Logic.ModCards;
using MLEM.Extensions;
using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic.Cards;

public class CreatureCard : Card
{
    private readonly ObservableCollection<ModCard> _mods = new();
    
    public int Life {get; private set; }

    public int Damage { get; set; }

    public IReadOnlyList<ModCard> Mods => _mods;

    public IObservableCollection<ModCard> ModsEvents => _mods;

    public CreatureCard(CardContent content) : base(content)
    {
        Life = content.Life;
        Damage = Content.Damage;
    }

    public void Initialization()
    {
        foreach (var initialMod in Content.Mods)
            Add(CardLoader.Get(initialMod) as ModCard);
    }

    public void Add(ModCard mod)
    {
        var existingMod = _mods.FirstOrDefault(x => x.GetType() == mod.GetType());

        if (existingMod is null)
        {
            var copy = mod.GetInitializedClone();
            copy.Canceled += card => _mods.Remove(card);
            _mods.Add(copy);
            copy.InitializeWithParent(this);
        }
        else
            existingMod.Append(mod);
    }

    public void Turn(ObservableCollection<CreatureCard> ownBoard, ObservableCollection<CreatureCard> againstBoard, Action postTurn)
    {
        var creature = Random.Shared.GetRandomEntry(againstBoard);
        var animation = new StrikeCardAnimation(this, creature);
        animation.Finished += x =>
        {
            creature.Hurt(Damage);
            postTurn();
        };
        Game1.Instance.Animations.Add(animation);
    }

    public void Hurt(int value)
    {
        foreach (var mod in _mods.ToList())
            value = mod.PreDamage(value);
        Life = Math.Max(0, Life - value);
        Damaged?.Invoke();
        if (Life == 0)
            Die();
    }

    protected override void FillDescription(StringBuilder builder)
    {
        base.FillDescription(builder);
        
        builder.Append("Attack: ");
        builder.AppendLine(Damage.ToString());
        
        builder.Append("Life: ");
        builder.AppendLine(Life.ToString());
    }

    public event Action<CreatureCard> Died;
    public event Action Damaged;

    private void Die()
    {
        Died?.Invoke(this);
    }
}