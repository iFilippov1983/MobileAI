using System;
using System.Collections.Generic;

public abstract class PlayerDataStock : IObservable<PlayerDataStock>, IDisposable
{
    private int _value;

    private List<IObserver<PlayerDataStock>> _enemyObservers = new List<IObserver<PlayerDataStock>>();

    public abstract DataType DataType { get; }

    public int Value 
    { 
        get => _value;
        set 
        {
            if (_value != value)
            { 
                _value = value;
                NotifyObservers();
            }
        }
    }

    public IDisposable Subscribe(IObserver<PlayerDataStock> observer)
    {
        _enemyObservers.Add(observer);
        return this;
    }

    public IDisposable Subscribe(params IObserver<PlayerDataStock>[] observers)
    { 
        foreach (var observer in observers)
            _enemyObservers.Add(observer);
        return this;
    }

    private void NotifyObservers()
    {
        foreach (var enemyObs in _enemyObservers)
            enemyObs.OnNext(this);
    }

    public void Dispose()
    {
        _enemyObservers.Clear();
    }
}

public class Money : PlayerDataStock
{
    public override DataType DataType => DataType.Money;
}

public class Health : PlayerDataStock
{
    public override DataType DataType => DataType.Health;
}

public class Power : PlayerDataStock
{
    public override DataType DataType => DataType.Power;
}

public class Stealth : PlayerDataStock
{
    public override DataType DataType => DataType.Stealth;
}
