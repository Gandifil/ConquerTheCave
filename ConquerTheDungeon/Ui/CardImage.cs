using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class CardImage: Image
{
    private readonly Texture2D _frame;
    
    public CardImage(Anchor anchor): base(anchor, Vector2.One, getTextureRegion(), true)
    {
        _frame = Game1.Instance.Content.Load<Texture2D>("images/card_frame");
        AddChild(new Image(Anchor.Center, new Vector2(500f, 500f), new TextureRegion(_frame)));
    }
    
    public CardImage(Anchor anchor, Vector2 size): base(anchor, size, getTextureRegion())
    {
        _frame = Game1.Instance.Content.Load<Texture2D>("images/card_frame");
        AddChild(new Image(Anchor.Center, new Vector2(1f, 1f), new TextureRegion(_frame)));
    }

    private static TextureRegion getTextureRegion()
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/cards/mage"));
    }
}