using ConquerTheDungeon.Logic;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace ConquerTheDungeon.Ui;

public class CardsPanel: Panel
{
    public CardsPanel(Anchor anchor, Vector2 size, Vector2 positionOffset, bool setHeightBasedOnChildren = false, bool scrollOverflow = false, bool autoHideScrollbar = true) : base(anchor, size, positionOffset, setHeightBasedOnChildren, scrollOverflow, autoHideScrollbar)
    {
        Style = new StyleProp<UiStyle>();
    }

    public void Add(CreatureCard card)
    {
        AddChild(new CardImage(card, Anchor.AutoInline, CardImage.GetSizeFromMlemWidth(0.1f)));
        AddChild(new Group(Anchor.AutoInline, new Vector2(10, 10)));
    }
}