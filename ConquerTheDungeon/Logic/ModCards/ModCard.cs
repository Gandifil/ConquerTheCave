using System;
using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public abstract class ModCard: Card
{
    public event Action<ModCard> Canceled;
        
    public ModCard(CardContent content) : base(content)
    {
    }

    public abstract bool CanBeAddedTo(CreatureCard creature);

    public abstract void Append(ModCard card);

    public abstract ModCard Clone();

    public void Cancel()
    {
        Canceled?.Invoke(this);
    }
}