using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace ConquerTheDungeon.Screens;

public class FightScreen: Screen
{
    private Texture2D _background;

    public override void Initialize()
    {
        base.Initialize();

        var root = new Panel(Anchor.BottomCenter, new Vector2(.95f, .33f), Vector2.Zero);
        Game1.Instance.UiSystem.Add("cards", root);
        root.AddChild(new CardImage(Anchor.Center, new Vector2(0.9f, 0.5f)));
    }
    
    public override void Dispose()
    {
        base.Dispose();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        _background = Game1.Instance.Content.Load<Texture2D>("images/backgrounds/darkest_cave_01");
    }

    public override void Update(GameTime gameTime)
    {
        //throw new System.NotImplementedException();
    }

    public override void Draw(GameTime gameTime)
    {
        Game1.Instance.SpriteBatch.Draw(_background, new Rectangle(0, 0, Game1.Instance.Graphics.PreferredBackBufferWidth, Game1.Instance.Graphics.PreferredBackBufferHeight), Color.White);
    }
}