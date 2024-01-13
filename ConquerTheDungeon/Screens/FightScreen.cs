using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;

namespace ConquerTheDungeon.Screens;

public class FightScreen: Screen
{
    private Texture2D _background;
    private CardsPanel _player_board;

    public override void Initialize()
    {
        base.Initialize();

        var root = new Panel(Anchor.BottomCenter, new Vector2(.95f, .33f), Vector2.Zero);
        root.AddChild(new CardImage(Anchor.Center, CardImage.GetSizeFromMlemWidth(0.1f)));
        
        Game1.Instance.UiSystem.Add("cards", root);
        Game1.Instance.UiSystem.Add("player_board", _player_board = 
            new CardsPanel(Anchor.Center, new Vector2(.95f, .33f), Vector2.Zero));
        
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
    }

    private void MouseOnMouseDragEnd(object sender, MouseEventArgs e)
    {
        var ui = Game1.Instance.UiSystem;
        var dragAndDrop = ui.Get(nameof(DragAndDrop));
        if (dragAndDrop is null) return;

        if (_player_board.DisplayArea.Contains(e.Position.ToVector2()))
        {
            _player_board.AddChild(new CardImage(Anchor.Center, new Vector2(0.5f, 0.5f)));
        }
    }

    public override void Dispose()
    {
        Game1.Instance.Mouse.MouseDragEnd -= MouseOnMouseDragEnd;
        
        base.Dispose();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        _background = Game1.Instance.Content.Load<Texture2D>("images/backgrounds/darkest_cave_01");
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
        Game1.Instance.SpriteBatch.Draw(_background, new Rectangle(0, 0, Game1.Instance.Graphics.PreferredBackBufferWidth, Game1.Instance.Graphics.PreferredBackBufferHeight), Color.White);
    }
}