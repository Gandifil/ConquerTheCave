using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerTheDungeon.Logic.Effects;

public class EffectsList<T>: Effect<T>, IEnumerable<Effect<T>>
{
    private readonly List<Effect<T>> _effects = new();

    public override string Description => CalculateDescription();

    public override void Commit(T item)
    {
        foreach (var effect in _effects)
            effect.Commit(item);
    }

    public override void Rollback(T item)
    {
        foreach (var effect in _effects)
            effect.Rollback(item);
    }

    private string CalculateDescription()
    {
        if (_effects.Count == 1)
            return _effects.First().Description;
        else
        {
            var builder = new StringBuilder();
            foreach (var effect in _effects)
                builder.AppendLine(effect.Description);
            return builder.ToString();
        }
    }
    
    public List<Effect<T>>.Enumerator GetEnumerator() => _effects.GetEnumerator();

    IEnumerator<Effect<T>> IEnumerable<Effect<T>>.GetEnumerator() => _effects.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _effects.GetEnumerator();
}