namespace ConquerTheDungeon.Logic.Cards.Spells;

public class HandgunShoot: SpellCard
{
    public int Damage => Content.Damage;
    
    public HandgunShoot(CardContent content) : base(content)
    {
    }

    public override void Use(CreatureCard target)
    {
        target.Hurt(Damage);
    }
}