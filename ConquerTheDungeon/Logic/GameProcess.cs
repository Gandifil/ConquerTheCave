using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic.Cards;
using MLEM.Extensions;
using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic;

public class GameProcess
{
    private readonly Player _player;
    private readonly List<Card> _backCards;
    private readonly ObservableCollection<Card> _currentCards;
    
    public readonly Board PlayerBoard = new();
    public readonly Board EnemyBoard = new();
    public IObservableCollection<Card> CurrentCardsEvents => _currentCards;
    public IReadOnlyCollection<Card> CurrentCards => _currentCards;

    public GameProcess(Player player)
    {
        _player = player;
        _backCards = player.Cards.ToList();
        _currentCards = new(_backCards);
    }

    public void Initialization()
    {
        foreach (var card in new FightScenario().GetInitialCards())
            EnemyBoard.Creatures.Add(card.Clone());
    }

    public void Use(Card card)
    {
        foreach (var item in _currentCards.ToList())
            if (item.GetType().IsAssignableTo(card.GetType()))
                _currentCards.Remove(item);
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

        RefreshCurrentCards();
    }

    private void RefreshCurrentCards()
    {
        foreach (var card in _backCards)
            if (!_currentCards.Contains(card))
                _currentCards.Add(card);
    }
}