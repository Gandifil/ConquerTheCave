using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace ConquerTheDungeon.Screens;

public class FightScreen: Screen
{
    private Texture2D _background;

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