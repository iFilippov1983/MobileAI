using System;
using UnityEngine;

[Serializable]
public struct EnemyProperties
{ 
    public string Name;
    public int K_Coins; //5
    public float K_Power; // 1.5f
    public float K_Stealth;
    public int ThresholdPlayersHealth; // 20
    public AnimationCurve PowerGrowthCurve;
}
