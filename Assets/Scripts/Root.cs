using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private FightWindowView _fightWindow;
    private Enemy _enemy;
    private Player _player;
    private FightController _fightController;

    private void Start()
    {
        _enemy = new Enemy(_enemyProperties);
        _player = new Player();
        _fightController = new FightController(_enemyProperties);
    }
}
