using System;
using UnityEngine;

[Serializable]
public class UnitData
{
    public int CountSpawnInRow;
    public int Row;
    public int StartHealth;
    public float ShotDistance; 
    public float Speed; 
    public float Damage; 

    public ECSMonoObject RedFighter;
    public ECSMonoObject BlueFighter;
    
    public Transform RedTeamStartPoint;
    public Transform BlueTeamStartPoint;
}