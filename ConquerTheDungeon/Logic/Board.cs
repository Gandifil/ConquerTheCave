using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic;

public class Board
{
    public readonly ObservableCollection<CreatureCard> Creatures = new();

    public Board()
    {
        Creatures.ItemAdded += (sender, args) => args.Item.Died += card => Creatures.Remove(card);
    }
}