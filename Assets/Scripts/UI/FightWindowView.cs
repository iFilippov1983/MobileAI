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
    [SerializeField] private TMP_Text _countStealthText;

    [SerializeField] private TMP_Text _countPowerEnemyText;
    [SerializeField] private TMP_Text _playerVisibilityText;

    [SerializeField] private TMP_Text _fightText;

    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;

    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;

    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;

    [SerializeField] private Button _addStealthButton;
    [SerializeField] private Button _minusStealthButton;

    [SerializeField] private Button _fightButton;

    private Enemy _enemy;
    private UiPlayerListener _uiListener;

    private Money _money;
    private Health _health;
    private Power _power;
    private Stealth _stealth;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Start()
    {
        _enemy = new Enemy(_enemyProperties);
        _uiListener = new UiPlayerListener(_countMoneyText, _countHealthText, _countPowerText, _countStealthText);

        _money = new Money();
        _money.Subscribe(_enemy, _uiListener, this);

        _health = new Health();
        _health.Subscribe(_enemy, _uiListener, this);

        _power = new Power();
        _power.Subscribe(_enemy, _uiListener, this);

        _stealth = new Stealth();
        _stealth.Subscribe(_enemy, _uiListener, this);

        _addMoneyButton.onClick.AddListener(() => ChangeValueOf(_money.DataType, 1));
        _minusMoneyButton.onClick.AddListener(() => ChangeValueOf(_money.DataType, -1));

        _addHealthButton.onClick.AddListener(() => ChangeValueOf(_health.DataType, 1));
        _minusHealthButton.onClick.AddListener(() => ChangeValueOf(_health.DataType, -1));

        _addPowerButton.onClick.AddListener(() => ChangeValueOf(_power.DataType, 1));
        _minusPowerButton.onClick.AddListener(() => ChangeValueOf(_power.DataType, -1));

        _addStealthButton.onClick.AddListener(() => ChangeValueOf(_stealth.DataType, 1));
        _minusStealthButton.onClick.AddListener(() => ChangeValueOf(_stealth.DataType, -1));

        _fightButton.onClick.AddListener(FightOrSneak);

        _disposables.Add(_money);
        _disposables.Add(_health);
        _disposables.Add(_power);

        UpdateEnemyDataWindow();
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addStealthButton.onClick.RemoveAllListeners();
        _minusStealthButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        foreach(var d in _disposables)
            d.Dispose();
    }

    private void FightOrSneak()
    {
        if (_enemy.SeesPlayer)
            Fight();
        else
            Sneak();
    }

    private void Sneak()
    {
        Debug.Log("<color=blue>..Sneaked..</color>");
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
            case DataType.Stealth:
                _stealth.Value += value;
                break;
        }
    }

    private void UpdateEnemyDataWindow()
    {
        _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
        _playerVisibilityText.text = _enemy.SeesPlayer
            ? "[Sees player]"
            : "[Doesn't see player]";

        _fightText.text = _enemy.SeesPlayer
            ? "FIGHT"
            : "SNEAK";
    }

    public void OnNext(PlayerDataStock playerData)
    {
        UpdateEnemyDataWindow();
    }

    public void OnCompleted() { }
    public void OnError(Exception error) => throw error;
}
