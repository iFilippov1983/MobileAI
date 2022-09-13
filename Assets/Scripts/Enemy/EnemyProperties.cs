using System;
using UnityEngine;

[Serializable]
public struct EnemyProperties
{ 
    public string Name;
    public int K_Coins; //5
    public float K_Power; // 1.5f
    public int ThresholdPlayersHealth; // 20
    public int StealthLevelToSkipPlayer;
    public AnimationCurve PowerGrowthCurve;
}
