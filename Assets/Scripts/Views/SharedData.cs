using System;
using UnityEngine;

[Serializable]
public class SharedData
{
    public int CountSpawnInRow;
    public int Row;
    public float ShotDistance; 

    public GameObject RedFighter;
    public GameObject BlueFighter;

    public GameObject BulletBlue;
    public GameObject BulletRed;

    public Transform RedTeamStartPoint;
    public Transform BlueTeamStartPoint;
}