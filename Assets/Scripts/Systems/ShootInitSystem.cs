using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

struct ShootInitSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<ArmyComponent, UnitTypeComponent, ShootComponent>> ecsFilterArmyUnitType;
    public void Init(IEcsSystems systems)
    {
        foreach (var entityIndex in ecsFilterArmyUnitType.Value)
        {                 
            ref var armyComponent = ref ecsFilterArmyUnitType.Pools.Inc1.Get(entityIndex);
            ref var unitTypeComponent = ref ecsFilterArmyUnitType.Pools.Inc2.Get(entityIndex);
            ref var shootComponent = ref ecsFilterArmyUnitType.Pools.Inc3.Get(entityIndex);

            if (armyComponent.TeamNumber == 0)        
                SetShootCompEntity (ref shootComponent, ref unitTypeComponent, "Red Team",  _data.Value.BulletBlue);
                      
            else if (armyComponent.TeamNumber == 1)           
                SetShootCompEntity(ref shootComponent, ref  unitTypeComponent, "Blue Team", _data.Value.BulletRed);                          
        }
    }

    private void SetShootCompEntity(ref ShootComponent poolShoot, ref UnitTypeComponent unit, string targetTag, GameObject bullet)
    {
        poolShoot.Spawn = unit.View.transform;
        poolShoot.TargetTag = targetTag;
        poolShoot.Bullet = bullet;
    }


}
