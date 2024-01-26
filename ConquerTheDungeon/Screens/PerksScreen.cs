using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Perks;
using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using MLEM.Ui.Elements;

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
                SetupPerk(j + N - perkLine.LeftPerks.Count, i, perkLine.LeftPerks[j]);
            SetupPerk(N, i, perkLine.Base);
            for (int j = 0; j < perkLine.RightPerks.Count; j++)
                SetupPerk(N + 1 + j, i, perkLine.RightPerks[j]);
        }
    }

    private void SetupPerk(int x, int y, Perk perk)
    {
        var image = new PerkImage(perk);
        _groups[x, y].AddChild(image);
    }

    public override void Dispose()
    {
        Game1.Instance.UiSystem.Remove("panel");
        base.Dispose();
    }
}