using ConquerTheDungeon.Logic.Perks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace ConquerTheDungeon.Ui;

public class PerkImage: Image
{
    public PerkImage(PerksMap.PerkHandler perk) 
        : base(Anchor.Center, new Vector2(0.9f), LoadTexture(perk.Perk.ImageName))
    {
        SetEnableState(perk.IsEnabled);

        perk.Enabled += PerkOnEnabled;
        OnRemovedFromUi += element => perk.Enabled -= PerkOnEnabled;
    }

    private void PerkOnEnabled(Perk obj)
    {
        SetEnableState(true);
    }

    private void SetEnableState(bool state)
    {
        DrawAlpha = state ? 1f : .5f;
    }

    private static TextureRegion LoadTexture(string perkImageName)
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/perks/" + perkImageName));
    }
}