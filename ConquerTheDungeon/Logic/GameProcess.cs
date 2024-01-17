using System;
using MLEM.Extensions;

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
            EnemyBoard.Creatures.Add(card.Clone());
    }

    public void Turn()
    {
        foreach (var card in EnemyBoard.Creatures)
            card.Turn(EnemyBoard.Creatures, PlayerBoard.Creatures);
        
        foreach (var card in PlayerBoard.Creatures)
            card.Turn(PlayerBoard.Creatures, EnemyBoard.Creatures);
    }
}