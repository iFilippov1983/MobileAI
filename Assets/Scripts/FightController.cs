using System;
using System.Collections.Generic;

public class FightController
{
    private EnemyProperties _enemyProperties;
    private Enemy _enemy;

    private Player _player;

    private List<IDisposable> _disposables = new List<IDisposable>();

    public FightController(EnemyProperties enemyProperties)
    {
        _enemyProperties = enemyProperties;
        _enemy = new Enemy(_enemyProperties);
        _player = new Player();
    }

    //...
}
