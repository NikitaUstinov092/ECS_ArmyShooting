using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class UnitData
{
    public int CountSpawnInRow;
    public int Row;
    public int StartHealth;
    public float ShotDistance; 
    public float Speed; 
    public float Damage; 

    [FormerlySerializedAs("RedFighter")] public ECSMonoObject RedUnit;
    [FormerlySerializedAs("BlueFighter")] public ECSMonoObject BlueUnit;
    
    public Transform RedTeamStartPoint;
    public Transform BlueTeamStartPoint;
}