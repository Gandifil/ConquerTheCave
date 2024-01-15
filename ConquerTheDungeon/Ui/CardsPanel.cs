using ConquerTheDungeon.Logic;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace ConquerTheDungeon.Ui;

public class CardsPanel: Group
{
    public CardsPanel(Board board, Anchor anchor, Vector2 size) : base(anchor, size, false)
    {
        board.Creatures.ItemAdded += (sender, args) => Add(args.Item);
    }

    private void Add(CreatureCard card)
    {
        AddCardImage(card);
        AddChild(new Group(Anchor.AutoInline, new Vector2(10, 10)));
    }

    private void AddCardImage(CreatureCard card)
    {
        var cardImage = new CardImage(card, Anchor.AutoInline, CardImage.GetSizeFromMlemWidth(0.1f));
        card.OnModAdded += (sender, modCard) =>
        {
            cardImage.AddChild(new CardImage(modCard, Anchor.Center, new Vector2(0.3f, 0f))
            {
                SetHeightBasedOnAspect = true,
                PositionOffset = new Vector2(0, 100f),
            });
        };
        AddChild(cardImage);
    }
}