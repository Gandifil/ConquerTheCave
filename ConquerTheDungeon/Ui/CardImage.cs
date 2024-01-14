using System;
using ConquerTheDungeon.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class CardImage: Image
{
    public static Size2 Size = new Size2(350, 500);

    public readonly Card Card;
    private readonly Texture2D _frame;
    private readonly Image _highlight;
    
    public CardImage(Card card, Anchor anchor, Vector2 size, bool canMove = true): base(anchor, size, getTextureRegion(card.Content.Texture))
    {
        Card = card;
        _frame = Game1.Instance.Content.Load<Texture2D>("images/" + (Card is CreatureCard ? "creature_card_frame" : "card_frame") );
        AddChild(new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(_frame)));
        if (canMove)
        {
            Game1.Instance.Mouse.MouseDragStart += MouseOnMouseDragStart;
            OnRemovedFromUi += element => { Game1.Instance.Mouse.MouseDragStart -= MouseOnMouseDragStart; };
        }

        if (Card is CreatureCard creatureCard)
        {
            AddChild(new Paragraph(Anchor.BottomLeft, 25, paragraph => creatureCard.Life.ToString())
            {
                PositionOffset = new Vector2(5, 0)
            });
            AddChild(new Paragraph(Anchor.BottomRight, 25, paragraph => creatureCard.Damage.ToString()));    
        }

        var highlightFrame = Game1.Instance.Content.Load<Texture2D>("images/card_frame_moused");
        AddChild(_highlight = new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(highlightFrame)));
        _highlight.IsHidden = true;
        CanBeMoused = true;
        OnMouseEnter = element => _highlight.IsHidden = false;
        OnMouseExit = element => _highlight.IsHidden = true;
    }

    private void MouseOnMouseDragStart(object sender, MouseEventArgs e)
    {
        if (DisplayArea.Contains(e.Position.ToVector2()))
        {
            Root.System.Add(nameof(DragAndDrop), new DragAndDrop(Card, DisplayArea.Size, e.Position.ToVector2() - DisplayArea.Location));
        }
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