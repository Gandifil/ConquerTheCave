using System.Collections.Generic;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class CardsPanel: Group
{
    private readonly Dictionary<CreatureCard, Group> _groups = new();
    public CardsPanel(Board board, Anchor anchor, Vector2 size) : base(anchor, size, false)
    {
        board.Creatures.ItemAdded += (sender, args) => Add(args.Item);
    }

    public bool CardsCanBeMoused
    {
        set
        {
            foreach (var creatureCard in _groups.Keys)
                creatureCard.UiElement.CanBeMoused = value;
        }
    }

    private void Add(CreatureCard card)
    {
        AddCardImage(card);
        AddChild(new Group(Anchor.AutoInline, new Vector2(10, 10)));
    }

    private void AddCardImage(CreatureCard card)
    {
        var cardImage = new CardImage(card, Anchor.AutoInline, new Vector2(1f, 1f))
        {
            SetHeightBasedOnAspect = true,
        };
        card.OnModAdded += (sender, modCard) =>
        {
            cardImage.AddChild(new CardImage(modCard, Anchor.Center, new Vector2(0.3f, 0f))
            {
                SetHeightBasedOnAspect = true,
                PositionOffset = new Vector2(0, 100f),
            });
        };
        var group = new Group(Anchor.AutoInline, new Vector2(.1f, 0));
        group.AddChild(cardImage);
        AddChild(group);
        _groups[card] = group;
        card.Died += CardOnDied;
    }

    private void CardOnDied(CreatureCard obj)
    {
        RemoveChild(_groups[obj]);
    }
}