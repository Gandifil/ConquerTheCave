using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Perks;
using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using MLEM.Formatting;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace ConquerTheDungeon.Screens;

public class PerksScreen: BackgroundScreen
{
    private const int N = 5;
    private const int LENGTH = 5;
    private readonly Player _player;
    private Group[,] _groups;
    
    public PerksScreen(Player player): base("campfire_01")
    {
        _player = player;
    }

    public override void Initialize()
    {
        base.Initialize();

        var panel = new Group(0, new Vector2(1f, 1f), false);

        Game1.Instance.UiSystem.Add("panel", panel);

        _groups = ElementHelper.MakeGrid(panel, new Vector2(0.8f), 2 * N + 1, LENGTH);
        for (int i = 0; i < _player.PerksMap.Lines.Length; i++)
        {
            var perkLine = _player.PerksMap.Lines[i];
            for (int j = 0; j < perkLine.LeftPerks.Count; j++)
                SetupPerk(N - 1 - j, i, perkLine.LeftPerks[j]);
            SetupPerk(N, i, perkLine.Base);
            for (int j = 0; j < perkLine.RightPerks.Count; j++)
                SetupPerk(N + 1 + j, i, perkLine.RightPerks[j]);
        }

        panel.AddChild(new Paragraph(Anchor.BottomLeft, 0.2f, 
            paragraph => "Очки перков: " + _player.PerkPoints, TextAlignment.Center)
        {
            TextScale = new StyleProp<float>(3f),
            TextColor = Color.White,
        });

        panel.AddChild(new Button(Anchor.BottomRight, new Vector2(0.2f, 0.1f), "Продолжить")
        {
            OnPressed = element => Game1.Instance.ScreenManager.LoadScreen(new FightScreen(_player))
        });
    }

    private void SetupPerk(int x, int y, PerkHandler perk)
    {
        var image = new PerkImage(perk);
        _groups[x, y].AddChild(image);

        if (x != N) // it's a perk which player can apply
        {
            image.CanBePressed = true;
            image.OnPressed += element => _player.TeachPerk(perk);
        }
    }

    public override void Dispose()
    {
        Game1.Instance.UiSystem.Remove("panel");
        base.Dispose();
    }
}