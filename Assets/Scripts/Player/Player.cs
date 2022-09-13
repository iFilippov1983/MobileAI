using System;
using System.Collections.Generic;

public class Player
{
    private Money _money;
    private Health _health;
    private Power _power;
    private Stealth _stealth;

    private List<PlayerDataStock> _stocks;
    private List<IDisposable> _disposables; 

    public Player()
    {
        InitStocks();
        MakeStocksList();
        MakeDisposablesList();
    }

    public void SubscribeObserver(IObserver<PlayerDataStock> observer)
    { 
        _money.Subscribe(observer);
        _health.Subscribe(observer);
        _power.Subscribe(observer);
        _stealth.Subscribe(observer);
    }

    public void SubscribeObserver<T>(IObserver<PlayerDataStock> observer) where T : PlayerDataStock
    {
        var stock = _stocks.Find(s => s.GetType().Equals(typeof(T)));
        stock.Subscribe(observer);
    }

    public void SubscribeObservers(params IObserver<PlayerDataStock>[] observers)
    {
        foreach (var stock in _stocks)
            stock.Subscribe(observers);
    }

    private void InitStocks()
    { 
        _money = new Money();
        _health = new Health();
        _power = new Power();
        _stealth = new Stealth();
    }

    private void MakeStocksList()
    { 
        _stocks = new List<PlayerDataStock>();

        _stocks.Add(_money);
        _stocks.Add(_health);
        _stocks.Add(_power);
        _stocks.Add(_stealth);
    }

    private void MakeDisposablesList()
    { 
        _disposables = new List<IDisposable>();

        _disposables.Add(_money);
        _disposables.Add(_health);
        _disposables.Add(_power);
        _disposables.Add(_power);
    }
}
