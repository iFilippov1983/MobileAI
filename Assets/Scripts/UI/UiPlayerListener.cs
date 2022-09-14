using System;
using TMPro;

public class UiPlayerListener : IEnemyDataUpdater, IObserver<PlayerDataStock>
{
    private readonly TMP_Text _moneyText;
    private readonly TMP_Text _healthText;
    private readonly TMP_Text _powerText;
    private readonly TMP_Text _stealthText;

    public UiPlayerListener(TMP_Text moneyText, TMP_Text healthText, TMP_Text powerText, TMP_Text stealthText)
    {
        _moneyText = moneyText;
        _healthText = healthText;
        _powerText = powerText;
        _stealthText = stealthText;
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
            case DataType.Money:
                _moneyText.text = $"Player money: {playerData.Value}";
                break;
            case DataType.Health:
                _healthText.text = $"Player health: {playerData.Value}";
                break;
            case DataType.Power:
                _powerText.text = $"Player power: {playerData.Value}";
                break;
            case DataType.Stealth:
                _stealthText.text = $"Player stealth: {playerData.Value}";
                break;
        }
    }
}