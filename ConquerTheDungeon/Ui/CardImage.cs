using System;
using System.Linq;
using ConquerTheDungeon.Animations;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.ModCards;
using ConquerTheDungeon.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class CardImage: Image
{
    public static Size2 Size = new Size2(350, 500);

    public event EventHandler<MouseEventArgs> OnMouseDrag;

    public readonly ObservedParameter<bool> CanBeChoosed = new();

    public readonly Card Card;
    private readonly Texture2D _frame;
    private readonly Image _highlight;

    public Color ColorProp
    {
        get => Color.Value;
        set
        {
            Color = new StyleProp<Color>(value);
        }
    }
    
    public CardImage(Card card, Anchor anchor, Vector2 size, bool canMove = false): base(anchor, size, getTextureRegion(card.Content.Texture))
    {
        Card = card;
        if (Card.UiElement is null)
            Card.UiElement = this;
        else
            throw new ApplicationException(nameof(Card.UiElement) + " must be null");
        _frame = Game1.Instance.Content.Load<Texture2D>("images/" + (Card is CreatureCard ? "creature_card_frame" : "card_frame") );
        AddChild(new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(_frame)));
        if (canMove)
        {
            Game1.Instance.Mouse.MouseDragStart += MouseOnMouseDragStart;
            OnRemovedFromUi += element =>
            {
                Game1.Instance.Mouse.MouseDragStart -= MouseOnMouseDragStart;
                Card.UiElement = null;
            };
        }

        if (Card is CreatureCard creatureCard)
        {
            AddChild(new Paragraph(Anchor.BottomLeft, 25, paragraph => creatureCard.Life.ToString())
            {
                PositionOffset = new Vector2(5, 0)
            });
            AddChild(new Paragraph(Anchor.BottomRight, 25, paragraph => creatureCard.Damage.ToString()));

            creatureCard.Damaged += () =>
            {
                var animation = new HurtCardAnimation(this);
                Game1.Instance.Animations.Add(animation);
            };
            
            creatureCard.ModsEvents.ItemAdded += ModsEventsOnItemAdded;
            creatureCard.ModsEvents.ItemRemoved += ModsEventsOnItemRemoved;
        }

        if (Card is ModCard modCard )
        {
            if (modCard is CounterModCard counterModCard)
            {
                if (counterModCard.Value != 0)
                    AddChild(new Paragraph(Anchor.BottomCenter, 50, paragraph => counterModCard.Value.ToString())
                    {
                        PositionOffset = new Vector2(0, -50f),
                        TextColor = new StyleProp<Color>(Microsoft.Xna.Framework.Color.LightGreen),
                        TextScaleMultiplier = 2f
                    });
            }
        }

        var highlightFrame = Game1.Instance.Content.Load<Texture2D>("images/card_frame_moused");
        AddChild(_highlight = new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(highlightFrame)));
        _highlight.IsHidden = true;
        OnMouseEnter = element => SetHighlightState(CanBeChoosed);
        OnMouseExit = element => SetHighlightState(false);
        CanBeChoosed.Changed += value => SetHighlightState(value && IsMouseOver);
        
        var _tooltip = new Tooltip(x => Card.Description, this);
        _tooltip.MouseOffset = new Vector2(32, -64);
        _tooltip.ParagraphWidth = new StyleProp<float>(300);
        
        CanBeMoused = true;
    }

    private void ModsEventsOnItemAdded(object sender, ItemEventArgs<ModCard> e)
    {
        AddChild(new CardImage(e.Item, Anchor.Center, new Vector2(0.3f, 0f))
        {
            SetHeightBasedOnAspect = true,
            PositionOffset = new Vector2(0, 100f),
        });
        UpdateModCardsPosition();
    }

    private void ModsEventsOnItemRemoved(object sender, ItemEventArgs<ModCard> e)
    {
        var cardImageChildren = Children.OfType<CardImage>();
        var cardImage = cardImageChildren.First(x => x.Card == e.Item);
        RemoveChild(cardImage);
        UpdateModCardsPosition();
    }

    private void UpdateModCardsPosition()
    {
        var y = DisplayArea.Height * .75f;
        var x = DisplayArea.Width * .3f;
        
        var cardImageChildren = Children.OfType<CardImage>().ToList();

        for (int i = 0; i < cardImageChildren.Count; i++)
            cardImageChildren[i].PositionOffset = new Vector2(i * x - (cardImageChildren.Count - 1) * x / 2, y);
    }

    private void MouseOnMouseDragStart(object sender, MouseEventArgs e)
    {
        if (DisplayArea.Contains(e.Position.ToVector2()))
        {
            OnMouseDrag?.Invoke(this, e);
        }
    }

    private void SetHighlightState(bool value)
    {
        _highlight.IsHidden = !value;
    }
    
    private static TextureRegion getTextureRegion(string contentTexture)
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/cards/" + contentTexture));
    }

    public static Vector2 GetSizeFromMlemWidth(float width)
    {
        return new Vector2(width, -Size.Height / Size.Width);
    }
}