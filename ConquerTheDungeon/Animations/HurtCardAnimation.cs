using ConquerTheDungeon.Logic.Cards;
using ConquerTheDungeon.Ui;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tweening;

namespace ConquerTheDungeon.Animations;

public class HurtCardAnimation: AnimationActor
{
    private readonly CardImage _cardImage;
    
    public HurtCardAnimation(CardImage cardImage)
    {
        _cardImage = cardImage;
    }
    
    public override void Start()
    {
        const float duration = .2f;
        _cardImage.ColorProp = Color.White;
        Game1.Instance.Animations.Tweener
            .TweenTo(_cardImage, x => x.ColorProp, 
                Color.Red, duration)
            .Easing(EasingFunctions.Linear)
            .AutoReverse()
            .OnEnd(_ => Finish());
        Game1.Instance.Animations.Tweener
            .TweenTo(_cardImage, x => x.PositionOffset,
                _cardImage.PositionOffset - new Vector2(0, 10), duration)
            .Easing(EasingFunctions.Linear)
            .AutoReverse();
        base.Start();
    }

    public override void Finish()
    {
        base.Finish();
    }
}