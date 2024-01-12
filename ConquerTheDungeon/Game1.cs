using ConquerTheDungeon.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace ConquerTheDungeon;

public class Game1 : Game
{
    public SpriteBatch SpriteBatch { get; private set; }
    public static Game1 Instance;
    
    public ScreenManager ScreenManager { get; private set; }
    public GraphicsDeviceManager Graphics { get; private set; }

    public Game1()
    {
        Instance = this;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Graphics = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        Graphics.IsFullScreen = false;
        Graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width - 50;
        Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 100;
        Graphics.ApplyChanges();

        Components.Add(ScreenManager = new ScreenManager());
        ScreenManager.LoadScreen(new FightScreen());

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        base.Draw(gameTime);
        SpriteBatch.End();
    }
}