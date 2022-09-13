using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class FightWindowView : MonoBehaviour, IObserver<PlayerDataStock>
{
    [SerializeField] private EnemyProperties _enemyProperties;

    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    [SerializeField] private TMP_Text _countPowerText;

    [SerializeField] private TMP_Text _countPowerEnemyText;

    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;

    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;

    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;

    [SerializeField] private Button _fightButton;

    private Enemy _enemy;
    private UiPlayerListener _uiListener;

    private Money _money;
    private Health _health;
    private Power _power;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Start()
    {
        _enemy = new Enemy(_enemyProperties);
        _uiListener = new UiPlayerListener(_countMoneyText, _countHealthText, _countPowerText);

        _money = new Money();
        _money.Subscribe(_enemy, _uiListener, this);
        //_money.Subscribe(_enemy);
        //_money.Subscribe(_uiListener);
        //_money.Subscribe(this);


        _health = new Health();
        _health.Subscribe(_enemy);
        _health.Subscribe(_uiListener);
        _health.Subscribe(this);

        _power = new Power();
        _power.Subscribe(_enemy);
        _power.Subscribe(_uiListener);
        _power.Subscribe(this);

        _addMoneyButton.onClick.AddListener(() => ChangeValueOf(_money.DataType, 1));
        _minusMoneyButton.onClick.AddListener(() => ChangeValueOf(_money.DataType, -1));

        _addHealthButton.onClick.AddListener(() => ChangeValueOf(_health.DataType, 1));
        _minusHealthButton.onClick.AddListener(() => ChangeValueOf(_health.DataType, -1));

        _addPowerButton.onClick.AddListener(() => ChangeValueOf(_power.DataType, 1));
        _minusPowerButton.onClick.AddListener(() => ChangeValueOf(_power.DataType, -1));

        _fightButton.onClick.AddListener(Fight);

        _disposables.Add(_money);
        _disposables.Add(_health);
        _disposables.Add(_power);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        foreach(var d in _disposables)
            d.Dispose();
    }

    private void Fight()
    {
        Debug.Log(_power.Value >= _enemy.Power
            ? "<color=green>-= WIN =-</color>" 
            : "<color=red>)) Loose ((</color>");
    }

    private void ChangeValueOf(DataType dataType, int value)
    {
        switch (dataType)
        {
            case DataType.None:
                break;
            case DataType.Money:
                _money.Value += value;
                break;
            case DataType.Health:
                _health.Value += value;
                break;
            case DataType.Power:
                _power.Value += value;
                break;
        }
    }

    private void UpdateEnemyDataWindow() => _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    public void OnNext(PlayerDataStock playerData) => UpdateEnemyDataWindow();
    public void OnCompleted() { }
    public void OnError(Exception error) => throw error;
}
