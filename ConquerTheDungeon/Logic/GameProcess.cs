namespace ConquerTheDungeon.Logic;

public class GameProcess
{
    private readonly Player _player;
    public readonly Board PlayerBoard = new();
    
    public readonly Board EnemyBoard = new();

    public GameProcess(Player player)
    {
        _player = player;
    }

    public void Initialization()
    {
        foreach (var card in new FightScenario().GetInitialCards())
            EnemyBoard.Creatures.Add(card);
    }
}