using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public abstract class ModCard: Card
{
    public ModCard(CardContent content) : base(content)
    {
    }

    public abstract bool CanBeAddedTo(CreatureCard creature);

    public abstract void Append(ModCard card);

    public abstract ModCard Clone();
}