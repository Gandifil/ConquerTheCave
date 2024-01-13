using ConquerTheDungeon.Logic;
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

    private readonly Player _player;

    public FightScreen(Player player)
    {
        _player = player;
    }

    public override void Initialize()
    {
        base.Initialize();

        
        Game1.Instance.UiSystem.Add("playerDesk", GetPlayerDesk());
        Game1.Instance.UiSystem.Add("player_board", _player_board = 
            new CardsPanel(Anchor.Center, new Vector2(.95f, .33f), Vector2.Zero));
        
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
    }

    private Element GetPlayerDesk()
    {
        var root = new Panel(Anchor.BottomCenter, new Vector2(.95f, .33f), Vector2.Zero);
        foreach (var card in _player.Cards)
            root.AddChild(new CardImage(card, Anchor.AutoInline, CardImage.GetSizeFromMlemWidth(0.1f)));
        return root;
    }

    private void MouseOnMouseDragEnd(object sender, MouseEventArgs e)
    {
        var ui = Game1.Instance.UiSystem;
        var dragAndDrop = ui.Get(nameof(DragAndDrop));
        if (dragAndDrop is null) return;

        if (_player_board.DisplayArea.Contains(e.Position.ToVector2()))
        {
            var cardImage = dragAndDrop.Element as DragAndDrop;
            _player_board.AddChild(new CardImage(cardImage.Card, Anchor.Center, CardImage.GetSizeFromMlemWidth(0.1f)));
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