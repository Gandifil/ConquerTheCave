using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Ui;

namespace ConquerTheDungeon.Animations;

public class MoveCardAnimation: AnimationActor
{
    private readonly CreatureCard _obj;
    private readonly CreatureCard _target;
    
    public MoveCardAnimation(CreatureCard obj, CreatureCard target)
    {
        _obj = obj;
        _target = target;
    }
    
    public override void Start()
    {
        var objCardImage = _obj.UiElement as CardImage;
        var targetCardImage = _target.UiElement as CardImage;
        Game1.Instance.Animations.Tweener
            .TweenTo(objCardImage, x => x.PositionOffset, targetCardImage.DisplayArea.Location, 1f)
            .OnEnd(_ => Finish());
        base.Start();
    }
}