using System;

namespace ConquerTheDungeon.Utils;

public class ObservedParameter<T> where T:IEquatable<T>
{
    private T _value;

    public T Value
    {
        get { return _value; }
        set
        {
            if (_value.Equals(value)) return;
            _value = value;
            Changed?.Invoke(value);
        }
    }

    public ObservedParameter()
    {
            
    }

    public ObservedParameter(T x)
    {
        _value = x;
    }

    public delegate void ChangeCallback(T newValue);
    
    public event ChangeCallback Changed;

    public static implicit operator T(ObservedParameter<T> p)
    {
        return p._value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}