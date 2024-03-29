using System;
using System.Collections.Generic;
using System.Linq;
using ConquerTheDungeon.Logic.Ai;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.Cards.Spells;
using ConquerTheDungeon.Logic.ModCards;
using MonoGame.Extended.Collections;

namespace ConquerTheDungeon.Logic;

public class GameProcess
{
    public readonly Player Player;
    private readonly FightScenario _fightScenario;
    private readonly List<Card> _backCards;
    private readonly ObservableCollection<Card> _currentCards;
    
    public readonly Board PlayerBoard = new();
    public readonly Board EnemyBoard = new();
    public static GameProcess Instance;
    private int turnIndex;
    public IObservableCollection<Card> CurrentCardsEvents => _currentCards;
    public IReadOnlyCollection<Card> CurrentCards => _currentCards;

    public event EventHandler<bool> Finished;

    public GameProcess(Player player, FightScenario fightScenario)
    {
        Player = player;
        _fightScenario = fightScenario;
        _backCards = player.Cards.ToList();
        _currentCards = new(player.Cards.ToList());
        Instance = this;
    }

    public void Initialization()
    {
        EnemyUseCards(_fightScenario.GetCards(0, EnemyBoard));
    }

    private void EnemyUseCards(CreatureCard[] cards)
    {
        foreach (var card in cards)
            EnemyBoard.Creatures.Add(card.Clone() as CreatureCard);
    }

    public void Use(Card card)
    {
        Type[] types = { typeof(CreatureCard), typeof(ModCard), typeof(SpellCard) };
        var usedType = types.First(x => x.IsInstanceOfType(card));
        foreach (var item in _currentCards.ToList())
            if (usedType.IsInstanceOfType(item))
                _currentCards.Remove(item);
        if (card.Content.IsOneOff)
            _backCards.Remove(_backCards.First(x => x.Content == card.Content));
        if (!_currentCards.Any())
            Turn();
    }

    public void Turn()
    {
        turnIndex++;
        EnemyUseCards(_fightScenario.GetCards(turnIndex, EnemyBoard));
        
        
        if (!PlayerBoard.Creatures.Any())
        {
            Finished?.Invoke(this, false);
            return;
        }
        
        if (!EnemyBoard.Creatures.Any())
        {
            Finished?.Invoke(this, true);
            return;
        }
        
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