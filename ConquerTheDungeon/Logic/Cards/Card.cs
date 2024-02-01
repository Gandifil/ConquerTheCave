using System;
using System.Text;
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

    public virtual string Description
    {
        get
        {
            var builder = new StringBuilder();
            builder.AppendLine(Content.Name);
            FillDescription(builder);
            return builder.ToString();
        }
    }

    protected virtual void FillDescription(StringBuilder builder) { }

    public object Clone()
    {
        return Activator.CreateInstance(GetType(), Content);
    }
}