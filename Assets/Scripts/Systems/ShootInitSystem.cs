using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

struct ShootInitSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<ArmyComponent, UnitTypeComponent, ShootComponent>> _ecsFilterArmyUnitType;
    public void Init(IEcsSystems systems)
    {
        foreach (var entityIndex in _ecsFilterArmyUnitType.Value)
        {                 
            ref var armyComponent = ref _ecsFilterArmyUnitType.Pools.Inc1.Get(entityIndex);
            ref var unitTypeComponent = ref _ecsFilterArmyUnitType.Pools.Inc2.Get(entityIndex);
            ref var shootComponent = ref _ecsFilterArmyUnitType.Pools.Inc3.Get(entityIndex);

            if (armyComponent.TeamNumber == 0)        
                SetShootCompEntity (ref shootComponent, ref unitTypeComponent,  _data.Value.BulletBlue);
                      
            else if (armyComponent.TeamNumber == 1)           
                SetShootCompEntity(ref shootComponent, ref  unitTypeComponent, _data.Value.BulletRed);                          
        }
    }

    private void SetShootCompEntity(ref ShootComponent poolShoot, ref UnitTypeComponent unit, ECSMonoObject bullet)
    {
        poolShoot.Spawn = unit.View.transform;
        poolShoot.Bullet = bullet;
    }


}
