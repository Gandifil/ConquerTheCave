using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Logic.Cards;

public class Card
{
    public Element UiElement;
    
    public CardContent Content;

    public Card(CardContent content)
    {
        Content = content;
    }
}