using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class CardImage: Image
{
    public CardImage(Anchor anchor): base(anchor, Vector2.One, getTextureRegion(), true)
    {
        
    }

    private static TextureRegion getTextureRegion()
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/cards/mage"));
    }
}