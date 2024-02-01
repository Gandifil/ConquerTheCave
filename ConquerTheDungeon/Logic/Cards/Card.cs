using System;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Logic.Cards;

public class Card: ICloneable
{
    public Element UiElement;
    
    public CardContent Content;

    public Card(CardContent content)
    {
        Content = content;
    }

    public object Clone()
    {
        return Activator.CreateInstance(GetType(), Content);
    }
}