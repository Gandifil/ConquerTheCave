using ConquerTheDungeon.Logic.Perks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class PerkImage: Image
{
    public PerkImage(Perk perk) 
        : base(Anchor.Center, new Vector2(1f), LoadTexture(perk.ImageName))
    {
    }

    private static TextureRegion LoadTexture(string perkImageName)
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/perks/" + perkImageName));
    }
}