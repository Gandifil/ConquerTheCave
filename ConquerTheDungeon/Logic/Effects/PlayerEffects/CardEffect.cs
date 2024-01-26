using System.Linq;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Logic.Effects.CardEffects;

namespace ConquerTheDungeon.Logic.Effects.PlayerEffects;

public class CardEffect : Effect<Player>
{
    public override string Description => $"Карта {CardId} получает : (\n{Effects.Description})";

    public readonly string CardId;

    public readonly EffectsList<Card> Effects;

    public CardEffect(string cardId, EffectsList<Card> effects)
    {
        CardId = cardId;
        Effects = effects;
    }

    public override void Commit(Player item)
    {
        Effects.Commit(FindCard(item));
    }

    public override void Rollback(Player item)
    {
        Effects.Rollback(FindCard(item));
    }

    private Card FindCard(Player item)
    {
        return item.Cards.First(x => x.Content.Id == CardId);
    }

    public static CardEffect GetAddDamageToCard(string cardId)
    {
        return new CardEffect(cardId, new EffectsList<Card> { new AddDamageEffect() });
    }
}