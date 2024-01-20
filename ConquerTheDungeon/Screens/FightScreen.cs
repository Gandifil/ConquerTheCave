using System;
using System.Diagnostics;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.Cards.Spells;
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
    private readonly GameProcess _gameProcess;

    public FightScreen(Player player)
    {
        _player = player;
        _gameProcess = new GameProcess(player);
    }

    public override void Initialize()
    {
        base.Initialize();

        
        Game1.Instance.UiSystem.Add("enemy_board", _enemyBoard = 
            new CardsPanel(_gameProcess.EnemyBoard, Anchor.TopCenter, new Vector2(1, .4f)));
        Game1.Instance.UiSystem.Add("player_board", _playerBoard = 
            new CardsPanel(_gameProcess.PlayerBoard, Anchor.Center, new Vector2(1, .4f)));
        Game1.Instance.UiSystem.Add("playerDesk", GetPlayerDesk());
        var turnButton = new Button(Anchor.BottomRight, new Vector2(0.1f, 0.1f), "Turn");
        turnButton.OnPressed = element => _gameProcess.Turn();
        Game1.Instance.UiSystem.Add("button", turnButton);
        
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;

        _gameProcess.Initialization();
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
            _playerBoard.CardsCanBeMoused = true;
        if (cardImage.Card is SpellCard spellCard)
        {
            if (spellCard.TargetKind == TargetKind.OneCard)
            {
                var board = GetBoard(spellCard);
                board.CardsCanBeMoused = true;
            }
        }
    }

    private CardsPanel GetBoard(SpellCard spellCard)
    {
        return spellCard.TargetSide switch
        {
            TargetSide.Player => _playerBoard,
            TargetSide.Enemy => _enemyBoard,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void MouseOnMouseDragEnd(object sender, MouseEventArgs e)
    {
        var ui = Game1.Instance.UiSystem;
        var root = ui.Get(nameof(DragAndDrop));
        if (root is null) return;
        var cardImage = root.Element as DragAndDrop;

        switch (cardImage.Card)
        {
            case CreatureCard c:
                if (_playerBoard.DisplayArea.Contains(e.Position.ToVector2()))
                    _gameProcess.PlayerBoard.Creatures.Add(c.Clone());
                break;
            case ModCard m:
                if (_playerBoard.DisplayArea.Contains(e.Position.ToVector2()))
                    foreach (var element in _playerBoard.GetChildren())
                        if (element.IsMouseOver)
                            if (element is CardImage elementCardImage)
                            {
                                var creature = elementCardImage.Card as CreatureCard;
                                creature.Add(m);
                            }
                break;
            
            case SpellCard spellCard:
                foreach (var element in GetBoard(spellCard).GetChildren())
                    if (element.IsMouseOver)
                        if (element is CardImage elementCardImage)
                            spellCard.Use(elementCardImage.Card as CreatureCard);
                break;
        }
        
        _playerBoard.CardsCanBeMoused = false;
        _enemyBoard.CardsCanBeMoused = false;
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