using UnityEngine;

public struct ShootComponent 
{
    public Transform Spawn;
    public const float Damage = 1;
    public string TargetTag;
    public ECSMonoObject Bullet;
}
