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
    private CardsPanel _playerBoard;
    private CardsPanel _enemyBoard;

    private readonly Player _player;

    public FightScreen(Player player)
    {
        _player = player;
    }

    public override void Initialize()
    {
        base.Initialize();

        
        Game1.Instance.UiSystem.Add("enemy_board", _enemyBoard = 
            new CardsPanel(Anchor.TopCenter, new Vector2(1, .4f)));
        foreach (var card in new FightScenario().GetInitialCards())
            _enemyBoard.Add(card);
        Game1.Instance.UiSystem.Add("player_board", _playerBoard = 
            new CardsPanel(Anchor.Center, new Vector2(1, .4f)));
        Game1.Instance.UiSystem.Add("playerDesk", GetPlayerDesk());
        
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
    }

    private Element GetPlayerDesk()
    {
        var root = new Panel(Anchor.BottomCenter, new Vector2(.95f, .2f), Vector2.Zero);
        foreach (var card in _player.Cards)
        {
            var cardImage = new CardImage(card, Anchor.AutoInline, new Vector2(0.05f, 0f), true)
            {
                SetHeightBasedOnAspect = true
            };
            cardImage.OnMouseDrag += CardOnMouseDrag;
            root.AddChild(cardImage);
        }
        
        return root;
    }

    private void CardOnMouseDrag(object sender, MouseEventArgs e)
    {
        var cardImage = sender as CardImage;
        Game1.Instance.UiSystem.Add(nameof(DragAndDrop), new DragAndDrop(cardImage.Card, 
            cardImage.DisplayArea.Size, 
            e.Position.ToVector2() - cardImage.DisplayArea.Location));
        if (cardImage.Card is ModCard)
            foreach (var element in _playerBoard.GetChildren())
                element.CanBeMoused = true;
    }

    private void MouseOnMouseDragEnd(object sender, MouseEventArgs e)
    {
        var ui = Game1.Instance.UiSystem;
        var root = ui.Get(nameof(DragAndDrop));
        if (root is null) return;
        var cardImage = root.Element as DragAndDrop;

        if (_playerBoard.DisplayArea.Contains(e.Position.ToVector2()))
        {
            switch (cardImage.Card)
            {
                case CreatureCard:
                    _playerBoard.Add(cardImage.Card as CreatureCard);
                    break;
                case ModCard:
                    foreach (var element in _playerBoard.GetChildren())
                        if (element.IsMouseOver)
                            if (element is CardImage elementCardImage)
                            {
                                var creature = elementCardImage.Card as CreatureCard;
                                creature.Add(cardImage.Card as ModCard);
                            }
                break;
                
            }
        }
        
        if (cardImage.Card is ModCard)
            foreach (var element in _playerBoard.GetChildren())
                element.CanBeMoused = false;
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