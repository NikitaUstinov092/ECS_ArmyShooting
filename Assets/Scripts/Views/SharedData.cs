using System;
using UnityEngine;

[Serializable]
public class SharedData
{
    public int CountSpawnInRow;
    public int Row;
    public float ShotDistance; 

    public ECSMonoObject RedFighter;
    public ECSMonoObject BlueFighter;

    public ECSMonoObject BulletBlue;
    public ECSMonoObject BulletRed;

    public Transform RedTeamStartPoint;
    public Transform BlueTeamStartPoint;
}