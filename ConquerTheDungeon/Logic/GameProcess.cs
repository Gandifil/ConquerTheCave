using System;
using System.Collections.Generic;
using ConquerTheDungeon.Logic.Cards;
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
        _dirtyCards.Clear();
        TurnNext();
    }

    private HashSet<Card> _dirtyCards = new ();

    private void TurnNext()
    {
        foreach (var card in EnemyBoard.Creatures)
            if (!_dirtyCards.Contains(card))
            {
                card.Turn(EnemyBoard.Creatures, PlayerBoard.Creatures, TurnNext);
                _dirtyCards.Add(card);
                return;
            }
        
        foreach (var card in PlayerBoard.Creatures)
            if (!_dirtyCards.Contains(card)) 
            {
                card.Turn(PlayerBoard.Creatures, EnemyBoard.Creatures, TurnNext);
                _dirtyCards.Add(card);
                return;
            }
    }
}