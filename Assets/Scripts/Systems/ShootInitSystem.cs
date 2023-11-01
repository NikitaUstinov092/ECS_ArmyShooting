using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Views;

struct ShootInitSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<UnitData> _unitData;
    private readonly EcsCustomInject<BulletData> _bulletData;
    
    private readonly EcsFilterInject<Inc<TeamComponent, UnitTypeComponent, ShootComponent>> _ecsFilterArmyUnitType;
    public void Init(IEcsSystems systems)
    {
        foreach (var entityIndex in _ecsFilterArmyUnitType.Value)
        {                 
            ref var armyComponent = ref _ecsFilterArmyUnitType.Pools.Inc1.Get(entityIndex);
            ref var unitTypeComponent = ref _ecsFilterArmyUnitType.Pools.Inc2.Get(entityIndex);
            ref var shootComponent = ref _ecsFilterArmyUnitType.Pools.Inc3.Get(entityIndex);

            switch (armyComponent.TeamType)
            {
                /*case 0:
                    SetShootCompEntity (ref shootComponent, ref unitTypeComponent,  _bulletData.Value.BulletBlue);
                    break;
                case 1:
                    SetShootCompEntity(ref shootComponent, ref  unitTypeComponent, _bulletData.Value.BulletRed);
                    break;*/
            }                          
        }
    }

    private void SetShootCompEntity(ref ShootComponent poolShoot, ref UnitTypeComponent unit, ECSMonoObject bullet)
    {
        poolShoot.Spawn = unit.View.transform;
        poolShoot.Bullet = bullet;
    }


}
