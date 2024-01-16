using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;

namespace ConquerTheDungeon.Animations;

public sealed class AnimationManager: SimpleGameComponent
{
    public readonly Tweener Tweener = new();
    private readonly Queue<AnimationActor> _queue = new();
    public AnimationActor Current => _queue.Peek();

    public void Add(AnimationActor animation)
    {
        animation.Finished += AnimationFinished;
        _queue.Enqueue(animation);
        if (_queue.Count == 1)
            Start();
    }

    private void AnimationFinished(AnimationActor obj)
    {
        _queue.Dequeue();
        if (_queue.Count > 0)
            Start();
    }

    private void Start()
    {
        Current.Start();
    }

    public override void Update(GameTime gameTime)
    {
        Tweener.Update(gameTime.GetElapsedSeconds());
    }
}