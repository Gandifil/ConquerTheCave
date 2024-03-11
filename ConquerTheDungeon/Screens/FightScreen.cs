using System;
using System.Linq;
using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Ai;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.Cards.Spells;
using ConquerTheDungeon.Logic.ModCards;
using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Screens;

public class FightScreen: BackgroundScreen
{
    private CardsPanel _playerBoard;
    private CardsPanel _enemyBoard;
    private PlayerCardsPanel _playerCardsPanel;

    private readonly GameProcess _gameProcess;

    public FightScreen(GameProcess gameProcess): base("darkest_cave_01")
    {
        _gameProcess = gameProcess;
    }

    public override void Initialize()
    {
        base.Initialize();

        _gameProcess.Finished += (sender, b) => 
            Game1.Instance.ScreenManager.LoadScreen(new FightScreen(new GameProcess(_gameProcess.Player, FightScenario.LoadFromContent(Game1.Instance.RoomMap.GetNext()))));
        
        Game1.Instance.UiSystem.Add("enemy_board", _enemyBoard = 
            new CardsPanel(_gameProcess.EnemyBoard, Anchor.TopCenter, new Vector2(1, .3f)));
        _enemyBoard.CardImageAdding += cardImage => 
            cardImage.PlayAnimation(new UiAnimation(.5f, (animation, element, percentage) => element.PositionOffset = new Vector2(0, -500 * (1 - percentage))));
        Game1.Instance.UiSystem.Add("player_board", _playerBoard = 
            new CardsPanel(_gameProcess.PlayerBoard, Anchor.Center, new Vector2(1, .3f))
            {
                PositionOffset = new Vector2(0, Game1.Instance.UiSystem.Viewport.Height / 12)
            });
        Game1.Instance.UiSystem.Add("player_desk", _playerCardsPanel = new PlayerCardsPanel(_gameProcess, Anchor.BottomCenter, new Vector2(.95f, .2f)));
        _playerCardsPanel.OnMouseDrag += CardOnMouseDrag;
        var turnButton = new Button(Anchor.BottomRight, new Vector2(0.1f, 0.1f), "Turn");
        turnButton.OnPressed = element => _gameProcess.Turn();
        Game1.Instance.UiSystem.Add("button", turnButton);
        
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
        Game1.Instance.Keys.KeyPressed += KeysOnKeyPressed;

        _gameProcess.Initialization();
    }

    public override void Dispose()
    {
        Game1.Instance.Mouse.MouseDragEnd -= MouseOnMouseDragEnd;
        Game1.Instance.Keys.KeyPressed -= KeysOnKeyPressed;

        var ui = Game1.Instance.UiSystem;
        ui.Remove("enemy_board");
        ui.Remove("player_board");
        ui.Remove("player_desk");
        ui.Remove("button");
        
        base.Dispose();
    }

    private void KeysOnKeyPressed(object sender, KeyboardEventArgs e)
    {
        if (e.Key is Keys.Enter or Keys.Space)
            _gameProcess.Turn();
    }

    private void CardOnMouseDrag(object sender, MouseEventArgs e)
    {
        var cardImage = sender as CardImage;
        Game1.Instance.UiSystem.Add(nameof(DragAndDrop), new DragAndDrop(cardImage.Card.Clone() as Card, 
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
        var root = Game1.Instance.UiSystem.Get(nameof(DragAndDrop));
        if (root is null) return;
        
        var dragAndDrop = root.Element as DragAndDrop;
        if (dragAndDrop is null) return;

        var usingCard = dragAndDrop.Card;
        switch (usingCard)
        {
            case CreatureCard creature:
                if (_playerBoard.DisplayArea.Contains(e.Position.ToVector2()))
                {
                    _gameProcess.PlayerBoard.Creatures.Add(creature.Clone() as CreatureCard);
                    _gameProcess.Use(usingCard);
                }
                break;
            case ModCard m:
                if (_playerBoard.DisplayArea.Contains(e.Position.ToVector2()))
                    foreach (var element in _playerBoard.GetCardImages())
                        if (element.IsMouseOver)
                        {
                            var creature = element.Card as CreatureCard;
                            creature.Add(m);
                            _gameProcess.Use(usingCard);
                        }
                break;
            
            case SpellCard spellCard:
                foreach (var element in GetBoard(spellCard).GetCardImages().ToList())
                    if (element.IsMouseOver)
                    {
                        spellCard.Use(element.Card as CreatureCard);
                        _gameProcess.Use(usingCard);
                    }
                break;
        }
        
        _playerBoard.CardsCanBeMoused = false;
        _enemyBoard.CardsCanBeMoused = false;
    }
}