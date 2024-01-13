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
}