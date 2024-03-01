using ConquerTheDungeon.Logic.Cards;

namespace ConquerTheDungeon.Logic.ModCards;

public class CreatingCreaturesWhenDieMod: StatelessModCard
{
    public int Count  => 3;

    public string Creature => "slime01";
    
    public CreatingCreaturesWhenDieMod(CardContent content) : base(content)
    {
    }

    public override void InitializeWithParent(CreatureCard creatureCard)
    {
        base.InitializeWithParent(creatureCard);
        
        creatureCard.Died += CreatureCardOnDied;
    }

    private void CreatureCardOnDied(CreatureCard obj)
    {
        var card = CardLoader.Get(Creature) as CreatureCard;
        for (int i = 0; i < Count; i++)
            GameProcess.Instance.EnemyBoard.Creatures.Add(card.Clone() as CreatureCard);
    }

    public override ModCard GetInitializedClone() => new CreatingCreaturesWhenDieMod(Content);
}