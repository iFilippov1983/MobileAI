using System;
using UnityEngine;

public class Enemy : IEnemyDataUpdater, IObserver<PlayerDataStock>
{
    EnemyProperties _properties;

    private string _name;
    private int _power;

    private int _moneyValuePlayer;
    private int _healthValuePlayer;
    private int _powerValuePlayer;

    public Enemy(EnemyProperties properties)
    {
        _properties = properties;
    }

    public int Power
    {
        get
        {
            var t = (float) 1 / (_powerValuePlayer + 1);
            var kHealth = _properties.PowerGrowthCurve.Evaluate(t) * 5;

            _power = (int)(_moneyValuePlayer / _properties.K_Coins + _healthValuePlayer / kHealth + _powerValuePlayer / _properties.K_Power + kHealth);
            return _power;
        }
    }

    public void OnCompleted() { }
    public void OnError(Exception error) => throw error;
    public void OnNext(PlayerDataStock playerData) => Update(playerData, playerData.DataType);

    public void Update(PlayerDataStock playerData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.None:
                break;

            case DataType.Health:
                _healthValuePlayer = playerData.Value;
                break;

            case DataType.Money:
                _moneyValuePlayer = playerData.Value;
                break;

            case DataType.Power:
                _powerValuePlayer = playerData.Value;
                break;
        }

        Debug.Log($"Enemy updated: {_name}; Data type changed: {dataType}");
    }
}
