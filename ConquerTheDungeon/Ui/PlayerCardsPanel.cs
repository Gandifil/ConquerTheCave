using System;
using System.Collections.Generic;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class PlayerCardsPanel: Panel
{
    private readonly GameProcess _gameProcess;
    public event EventHandler<MouseEventArgs> OnMouseDrag;
    
    public PlayerCardsPanel(GameProcess gameProcess, Anchor anchor, Vector2 size) : base(anchor, size)
    {
        _gameProcess = gameProcess;
        _gameProcess.CurrentCardsEvents.ItemAdded += CurrentCardsEventsOnItemAdded;
        _gameProcess.CurrentCardsEvents.ItemRemoved += CurrentCardsEventsOnItemRemoved;
        foreach (var card in _gameProcess.CurrentCards) Append(card);
    }

    private void CurrentCardsEventsOnItemAdded(object sender, ItemEventArgs<Card> e)
    {
        Append(e.Item);
    }

    private void CurrentCardsEventsOnItemRemoved(object sender, ItemEventArgs<Card> e)
    {
        RemoveChild(e.Item.UiElement);
    }

    private void Append(Card card)
    {
        var cardImage = new CardImage(card, Anchor.AutoInline, new Vector2(0.05f, 0f), true)
        {
            SetHeightBasedOnAspect = true
        };
        cardImage.OnMouseDrag += (sender, args) => OnMouseDrag?.Invoke(sender, args);
        AddChild(cardImage);
    }
}