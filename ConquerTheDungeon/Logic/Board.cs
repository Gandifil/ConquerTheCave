using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic;

public class Board
{
    public readonly ObservableCollection<CreatureCard> Creatures = new();
}