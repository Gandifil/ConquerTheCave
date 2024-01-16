using System;

namespace ConquerTheDungeon.Animations;

public abstract class AnimationActor
{
    public event Action<AnimationActor> Started;
    
    public event Action<AnimationActor> Finished;

    public virtual void Start()
    {
        Started?.Invoke(this);
    }

    public virtual void Finish()
    {
        Finished?.Invoke(this);
    }
}