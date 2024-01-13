using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class CardImage: Image
{
    private readonly Texture2D _frame;
    
    public CardImage(Anchor anchor, Vector2 size, bool canMove = true): base(anchor, size, getTextureRegion())
    {
        _frame = Game1.Instance.Content.Load<Texture2D>("images/card_frame");
        AddChild(new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(_frame)));
        if (canMove)
        {
            Game1.Instance.Mouse.MouseDragStart += MouseOnMouseDragStart;
            OnRemovedFromUi += element => { Game1.Instance.Mouse.MouseDragStart -= MouseOnMouseDragStart; };
        }
    }

    private void MouseOnMouseDragStart(object sender, MouseEventArgs e)
    {
        if (DisplayArea.Contains(e.Position.ToVector2()))
        {
            Root.System.Add(nameof(DragAndDrop), new DragAndDrop(0, DisplayArea.Size));
        }
    }
    
    private static TextureRegion getTextureRegion()
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/cards/mage"));
    }
}