using System;
using System.Text;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Logic.Cards;

public class Card: ICloneable, IFormattable
{
    public Element UiElement;
    
    public CardContent Content;

    public Card(CardContent content)
    {
        Content = content;
    }

    public string Description
    {
        get
        {
            var builder = new StringBuilder();
            FillDescription(builder);
            return builder.ToString();
        }
    }

    protected virtual void FillDescription(StringBuilder builder)
    {
        builder.AppendLine(Content.Name);
        if (!string.IsNullOrWhiteSpace(Content.Description))
            builder.AppendLine(string.Format(Content.Description, this));
    }

    public object Clone()
    {
        return Activator.CreateInstance(GetType(), Content);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return GetType().GetProperty(format ?? throw new ArgumentNullException(nameof(format)))
            ?.GetValue(this)?.ToString() ?? "null";
    }
}