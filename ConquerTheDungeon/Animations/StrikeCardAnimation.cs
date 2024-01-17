using ConquerTheDungeon.Logic;
using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tweening;

namespace ConquerTheDungeon.Animations;

public class StrikeCardAnimation: AnimationActor
{
    private readonly CreatureCard _obj;
    private readonly CardImage _objCardImage;
    private readonly CreatureCard _target;
    
    public StrikeCardAnimation(CreatureCard obj, CreatureCard target)
    {
        _obj = obj;
        _objCardImage = _obj.UiElement as CardImage;
        _target = target;
    }
    
    public override void Start()
    {
        var targetCardImage = _target.UiElement as CardImage;
        Game1.Instance.Animations.Tweener
            .TweenTo(_objCardImage, x => x.PositionOffset, 
                targetCardImage.DisplayArea.Location - _objCardImage.DisplayArea.Location, .2f)
            .OnEnd(_ => Finish())
            .Easing(EasingFunctions.QuadraticIn);
        base.Start();
    }

    public override void Finish()
    {
        _objCardImage.PositionOffset = Vector2.Zero;
        base.Finish();
    }
    
    
}