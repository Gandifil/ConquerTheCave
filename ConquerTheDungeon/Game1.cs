using ConquerTheDungeon.Animations;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Ai;
using ConquerTheDungeon.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Font;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Style;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;

namespace ConquerTheDungeon;

public class Game1 : Game
{
    public SpriteBatch SpriteBatch { get; private set; }
    public static Game1 Instance;
    
    public ScreenManager ScreenManager { get; private set; }
    public GraphicsDeviceManager Graphics { get; private set; }

    public MouseListener Mouse { get; private set; }

    public KeyboardListener Keys { get; private set; }

    public UiSystem UiSystem { get; private set; }

    public RoomMap RoomMap { get; private set; }

    public AnimationManager Animations { get; private set; }

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
        Components.Add(UiSystem = new UiSystem(this, new UiStyle()));

        Components.Add(new InputListenerComponent(this, Mouse = new MouseListener(), Keys = new KeyboardListener()));

        Components.Add(ScreenManager = new ScreenManager());

        Components.Add(Animations = new AnimationManager());
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        var testTexture = Content.Load<Texture2D>("images/ui_asset");
        var testPatch = new NinePatch(new TextureRegion(testTexture, 128, 0, 192, 192), 64);

        var style = new UntexturedStyle(this.SpriteBatch) {
            Font = new GenericSpriteFont(
                Content.Load<SpriteFont>("fonts/gui"), 
                Content.Load<SpriteFont>("fonts/gui"), 
                Content.Load<SpriteFont>("fonts/gui")),
            PanelTexture = testPatch,
            ButtonTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
            TextFieldTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
            ScrollBarBackground = new NinePatch(new TextureRegion(testTexture, 12, 0, 4, 8), 1, 1, 2, 2),
            ScrollBarScrollerTexture = new NinePatch(new TextureRegion(testTexture, 8, 0, 4, 8), 1, 1, 2, 2),
            CheckboxTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
            CheckboxCheckmark = new TextureRegion(testTexture, 24, 0, 8, 8),
            RadioTexture = new NinePatch(new TextureRegion(testTexture, 16, 0, 8, 8), 3),
            RadioCheckmark = new TextureRegion(testTexture, 32, 0, 8, 8),
            TextColor = Color.Black,
            PanelChildPadding = new Vector2(18)
        };
        UiSystem.Style = style;
        UiSystem.AutoScaleWithScreen = true;

        RoomMap = new RoomMap();
        ScreenManager.LoadScreen(new FightScreen(new GameProcess(new Player(), FightScenario.LoadFromContent(RoomMap.GetNext()))));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
        //UiSystem.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        base.Draw(gameTime);
        SpriteBatch.End();

        UiSystem.Draw(gameTime, SpriteBatch);
    }
}