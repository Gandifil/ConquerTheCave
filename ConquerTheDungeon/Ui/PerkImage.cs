using System;
using ConquerTheDungeon.Logic.Perks;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConquerTheDungeon.Ui;

public class PerkImage: Image
{
    private readonly Image _image;
    
    public PerkImage(PerkHandler perk) 
        : base(Anchor.Center, new Vector2(0.9f), LoadTexture(perk.Perk.ImageName))
    {
        SetEnableState(perk.IsEnabled);

        perk.Enabled += PerkOnEnabled;
        OnRemovedFromUi += element => perk.Enabled -= PerkOnEnabled;

        var texture = Game1.Instance.Content.Load<Texture2D>("images/selector_frame");
        AddChild(_image = new Image(Anchor.Center, Vector2.One, new TextureRegion(texture)));
        _image.Color = new StyleProp<Color>(Microsoft.Xna.Framework.Color.White);

        CanBeMoused = true;
        OnMouseEnter = element => _image.Color = new StyleProp<Color>(Microsoft.Xna.Framework.Color.Yellow);
        OnMouseExit = element => _image.Color = new StyleProp<Color>(Microsoft.Xna.Framework.Color.White);

        _image.IsHidden = !perk.CanEnable;
        perk.CanEnable.Changed += CanEnableOnChanged;
        OnRemovedFromUi += element => perk.CanEnable.Changed -= CanEnableOnChanged;
    }

    private void CanEnableOnChanged(bool newValue)
    {
        _image.IsHidden = !newValue;
    }

    private void PerkOnEnabled(Perk obj)
    {
        SetEnableState(true);
    }

    private void SetEnableState(bool state)
    {
        DrawAlpha = state ? 1f : .5f;
    }
    
    private float _timer = 0;

    public override void Update(GameTime time)
    {
        base.Update(time);

        _timer += time.GetElapsedSeconds();
        if (_timer > MathF.PI)
            _timer -= MathF.PI;
        _image.DrawAlpha = MathF.Sin(_timer);
    }

    private static TextureRegion LoadTexture(string perkImageName)
    {
        return new TextureRegion(Game1.Instance.Content.Load<Texture2D>("images/perks/" + perkImageName));
    }
}