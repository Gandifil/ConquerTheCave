using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class CardsPanel: Group
{
    private readonly Dictionary<CreatureCard, Group> _groups = new();

    public event Action<CardImage> CardImageAdding;
    
    public CardsPanel(Board board, Anchor anchor, Vector2 size) : base(anchor, size, false)
    {
        board.Creatures.ItemAdded += (sender, args) => Add(args.Item);
        board.Creatures.ItemRemoved += (sender, args) => CardOnDied(args.Item);
    }

    public bool CardsCanBeMoused
    {
        set
        {
            foreach (var creature in _groups.Keys)
                if (creature.UiElement is CardImage cardImage)
                    cardImage.CanBeChoosed.Value = value;
        }
    }

    private void Add(CreatureCard card)
    {
        AddCardImage(card);
        AddChild(new Group(Anchor.AutoInline, new Vector2(10, 10)));
        card.Initialization();
    }

    private void AddCardImage(CreatureCard card)
    {
        var cardImage = new CardImage(card, Anchor.AutoInline, new Vector2(1f, 1f))
        {
            SetHeightBasedOnAspect = true,
        };
        CardImageAdding?.Invoke(cardImage);
        var group = new Group(Anchor.AutoInline, new Vector2(.1f, 0));
        group.AddChild(cardImage);
        AddChild(group);
        _groups[card] = group;
    }
    
    /// <summary>
    /// Need use this, because this element include groups, which include CardImages.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CardImage> GetCardImages()
    {
        return Children.SelectMany(x => x.GetChildren().OfType<CardImage>());
    }

    private void CardOnDied(CreatureCard obj)
    {
        (obj.UiElement as CardImage)?.PlayDieAnimation((obj1) => RemoveCard(obj));
    }

    private void RemoveCard(CreatureCard obj)
    {
        RemoveChild(_groups[obj]);
        _groups.Remove(obj);
    }
}