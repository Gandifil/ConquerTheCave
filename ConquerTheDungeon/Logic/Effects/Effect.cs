namespace ConquerTheDungeon.Logic.Effects;

public abstract class Effect<T>
{
    public abstract string Description { get; }
    
    public virtual void Commit(T item) {}
    
    public virtual void Rollback(T item) {}
}